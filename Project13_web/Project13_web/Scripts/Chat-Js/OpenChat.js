
    function switchChat() {
        document.getElementById('community').addEventListener('click', function () {
            var titleElement = document.getElementById('title');
            if (titleElement) {
                titleElement.textContent = 'Community Books Lover';
            }


        });
        }


    function setTextInTitle() {
        document.getElementById('admin').addEventListener('click', function () {
            var titleElement = document.getElementById('title');
            if (titleElement) {
                titleElement.textContent = 'Admin Support';
            }
        });
        }






    document.getElementById('chatButton').addEventListener('click', function () {
            var titleElement = document.getElementById('title');
    if (titleElement) {
        titleElement.textContent = 'Community Books Lover';
            }
    setTextInTitle();
    switchChat();
    var popup = document.getElementById('chatPopup');
    popup.style.display = (popup.style.display === 'none' || popup.style.display === '') ? 'block' : 'none';



        });

