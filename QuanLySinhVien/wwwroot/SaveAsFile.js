window.base64ToArrayBuffer = (base64) => {
    const binaryString = window.atob(base64);
    const bytes = new Uint8Array(binaryString.length);

    for (let i = 0; i < binaryString.length; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }

    return bytes.buffer;
};

window.saveAsFile = (fileName, bytesBase64) => {
    const link = document.createElement('a');
    const blob = new Blob([base64ToArrayBuffer(bytesBase64)], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
};