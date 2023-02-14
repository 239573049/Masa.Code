function mousedown(id) {
    let box = document.getElementById(id);
    document.onmousedown = function (e) {
        let disx = e.pageX - box.offsetLeft;
        let disy = e.pageY - box.offsetTop;
        
        document.onmousemove = function (e) {
            box.style.left = e.pageX - disx + 'px';
            box.style.top = e.pageY - disy + 'px';
        }
        
        //释放鼠标按钮，将事件清空，否则始终会跟着鼠标移动
        document.onmouseup = function () {
            document.onmousemove = document.onmouseup = null;
        }
    }
}


export {
    mousedown
}