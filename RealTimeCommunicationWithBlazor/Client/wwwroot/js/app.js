(function () {

    window.formatTextEditor = () => {


        let buttons = document.getElementsByClassName('btn-textformat');

        for (let btn of buttons) {

            btn.addEventListener('click', () => {
                console.log("Clicked btn - ", btn);
                let cmd = btn.dataset['command'];
                document.execCommand(cmd, false, null);

            })
        }
    }

    window.getBlogContent = () => {

        console.log("Inside getBlogContent");
        const element = document.getElementById('dvBlog');
        const content = element.innerHTML;
        const text = element.innerText;
        return {
            content, text
        }
    }
})();