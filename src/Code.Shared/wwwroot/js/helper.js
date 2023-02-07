/** 将byte转url对象 */
function byteToUrl(blob) {
    // 适配webview和web 
    var myBlob = new Blob([blob], { type: "image/png" });
    return (window.URL || window.webkitURL || window || {}).createObjectURL(myBlob);
}
/**
 * 释放url对象，因为createObjectURL创建的对象一直会存在可能会占用过多的内存，请注意释放
 */
function revokeUrl(url) {
    (window.URL || window.webkitURL || window || {}).revokeObjectURL(url);
}

function onKeydown(dotNetHelper){
    document.onkeydown = async function (e) {
        const keyCode = e.keyCode || e.which || e.charCode;
        const ctrlKey = e.ctrlKey || e.metaKey;
        const result = await dotNetHelper.invokeMethodAsync("OnKeydown", ctrlKey, keyCode);
        if (result) {
            return true;
        }
        e.preventDefault();
        return false;
    }
}

export  {
    byteToUrl,
    revokeUrl,
    onKeydown
}