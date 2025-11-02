import ApiService from '../ApiService';

const RespaldosInformacionService = {
  createBackup(){
    return ApiService.post('/RespaldosDeInformacion/CreateBackup', {url:null,webRootPath:null});
  },
   restoreBackup({ file, overwrite, passphrase, onUploadProgress } = {}) {
    const form = new FormData();
    // 👇 el nombre DEBE matchear la propiedad del Command: BackupFile
    form.append('BackupFile', file);

    // Campos extra si existen en el Command
    if (overwrite !== undefined) form.append('Overwrite', String(!!overwrite));
    if (passphrase) form.append('Passphrase', passphrase);

    return ApiService.post('RespaldosDeInformacion/RestoreDatabase', form, {
      headers: { 'Content-Type': 'multipart/form-data' },
      onUploadProgress, // opcional: barra de progreso
    });
  },
};

export default RespaldosInformacionService;
