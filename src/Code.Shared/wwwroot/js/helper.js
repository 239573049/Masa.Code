/** 将byte转url对象 */
function byteToUrl(blob) {
    // 适配webview和web 
    var myBlob = new Blob([blob], {type: "image/png"});
    return (window.URL || window.webkitURL || window || {}).createObjectURL(myBlob);
}

/**
 * 释放url对象，因为createObjectURL创建的对象一直会存在可能会占用过多的内存，请注意释放
 */
function revokeUrl(url) {
    (window.URL || window.webkitURL || window || {}).revokeObjectURL(url);
}

function onKeydown(id, dotNetHelper) {
    document.getElementById(id)
        .onkeydown = async function (e) {
        const keyCode = e.keyCode || e.which || e.charCode;
        const ctrlKey = e.ctrlKey || e.metaKey;
        await dotNetHelper.invokeMethodAsync("OnKeydown", keyCode, ctrlKey);
        e.preventDefault();
        return false;
    }
}

function onDidBlurEditorText(editor, dotNetHelper, method) {
    editor.onDidBlurEditorText(async () => {
        await dotNetHelper.invokeMethodAsync(method)
    });
}

/***
 * 监听数据变化
 * @param editor
 * @param dotNetHelper
 * @param method
 */
function onDidChangeModelContent(editor, dotNetHelper, method) {
    window.mon = editor
    editor.onDidChangeModelContent(async (e) => {
        await dotNetHelper.invokeMethodAsync(method, e)
    });
}

/***
 * 监听显示行数
 * @param editor
 * @returns {*}
 */
function getVisibleRanges(editor) {
    return editor.getVisibleRanges();
}

/***
 * 监听monaco滚动条事件
 * @param editor
 * @param dotNetHelper
 * @param method
 */
function onDidScrollChange(editor, dotNetHelper, method) {
    editor.onDidScrollChange(async function (e) {
        await dotNetHelper.invokeMethodAsync(method)
    });
}

export {
    byteToUrl,
    revokeUrl,
    onKeydown,
    onDidScrollChange,
    getVisibleRanges,
    onDidBlurEditorText,
    onDidChangeModelContent
}