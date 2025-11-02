using System.IO.Compression;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shared.Application.Services.Interfaces;
using Shared.Application.Strategies;

namespace Application.FunctionalUnits.Auth.Commands.Strategies
{
    public record GenerarBackupRecord(string WebRootPath, string Url);

    public class GenerarBackupStrategy : BehaviorAdapterStrategy
    {
        private readonly string containerBackup = "backup";

        public GenerarBackupStrategy(IWorkContext workContext) : base(workContext) { }

        public override async Task<TResult> RunAsync<TResult>(object[] args)
        {
            if (typeof(TResult) != typeof(string))
                throw new ArgumentException("El resultado de la estrategia debe ser un string.");

            var record = ValidateArgs<GenerarBackupRecord>(args).Single();

            var urlArchivoComprimido = await GenerarBackup(record.WebRootPath, record.Url);
            return (TResult)(object)urlArchivoComprimido;
        }

        private async Task<string> GenerarBackup(string webRootPath, string baseUrl)
        {
            // Asegurar carpeta de backup dentro de wwwroot
            var backupDir = Path.Combine(webRootPath, containerBackup);
            Directory.CreateDirectory(backupDir);

            var backupZipName = "backup.zip";
            var zipFilePath = Path.Combine(backupDir, backupZipName);

            // Si existe, lo borro para crear desde cero
            if (File.Exists(zipFilePath))
                File.Delete(zipFilePath);

            // Ejecuta backup y comprime
            await BackupDatabase(webRootPath, zipFilePath);

            // Construir URL correctamente (NO usar Path.Combine para URLs)
            var zipUrl = new Uri(new Uri(EnsureTrailingSlash(baseUrl)),
                                 $"{containerBackup}/{backupZipName}").ToString();

            return zipUrl;
        }

        private static string EnsureTrailingSlash(string url)
            => url.EndsWith("/") ? url : url + "/";

        private async Task BackupDatabase(string webRootPath, string zipFilePath)
        {
            var databaseFolderName = "database";
            var tempFolder = Path.Combine(webRootPath, containerBackup, databaseFolderName);

            // Limpiar/crear carpeta temporal
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);

            try
            {
                // 1) Ejecuta el backup a la carpeta temporal
                await WorkContext.Services.UnitOfWork.BackupAsync(tempFolder);

                // 2) Comprime esa carpeta al ZIP destino
                AddToZip(zipFilePath, tempFolder, databaseFolderName);
            }
            finally
            {
                // 3) Limpieza
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
        }

        private void AddToZip(string zipFilePath, string folderToAdd, string destinationFolderInZip)
        {
            // Asegurar carpeta contenedora del ZIP
            var zipDir = Path.GetDirectoryName(zipFilePath);
            if (!string.IsNullOrEmpty(zipDir))
                Directory.CreateDirectory(zipDir);

            // Crear un ZIP nuevo desde cero
            using (var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (var file in Directory.EnumerateFiles(folderToAdd, "*", SearchOption.AllDirectories))
                {
                    var rel = Path.GetRelativePath(folderToAdd, file).Replace('\\', '/');

                    var entryName = string.IsNullOrWhiteSpace(destinationFolderInZip)
                        ? rel
                        : $"{destinationFolderInZip.Trim('/').Replace('\\', '/')}/{rel}";

                    zip.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                }
            }
        }
    }
}
