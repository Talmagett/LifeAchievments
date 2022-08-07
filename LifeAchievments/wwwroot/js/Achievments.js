/*
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});
*/

const searchBar = document.querySelector('.searching-input');
const achievmentModal = document.getElementById('achievment-modal');

achievmentModal.addEventListener('show.bs.modal', event => {
    const button = event.relatedTarget
    const categoryId = button.getAttribute('data-bs-whatever')
    fillModalData(categoryId);
})
console.log(document.getElementById('achievment-modal'));;
console.log(achievmentModal);;


function filterByName() {
    var value = searchBar.value.toLowerCase();
    var achievments = document.getElementById('achievments');

    var title, i, txtValue;
    var btns = achievments.getElementsByTagName("button");

    for (i = 0; i < btns.length; i++) {
        title = btns[i].querySelector('.card-title');
        if (title) {
            txtValue = title.textContent || title.innerText;
            if (txtValue.toLowerCase().indexOf(value) > -1) {
                btns[i].style.display = "";
            } else {
                btns[i].style.display = "none";
            }
        }
    }
}

/*   $("#achievments").filter(key=>key.
       function () {
       $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
   });*/


//Modal

function fillModalData(id) {
    achievmentModal.querySelector(".edit-achievment-id").setAttribute("asp-page-handler", id)
    var id = achievmentModal.querySelector(".edit-achievment-id").getAttribute("asp-page-handler");
    $.ajax({
        url: '/Achievments?handler=ProgressOfEditingId',
        type: 'get',
        data: {
            "id": id
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            //console.log(response);
            achievmentModal.querySelector('.modal-header .modal-title').innerHTML = response.name
            achievmentModal.querySelector('.modal-body .achievment-input-description').innerHTML = response.description
            achievmentModal.querySelector('.modal-body .achievment-input-award').value = response.award
            achievmentModal.querySelector('.modal-body .achievment-input-comment').innerHTML = response.comment
            achievmentModal.querySelector('.modal-body .achievment-input-maxAmount').value = response.maxAmount
            achievmentModal.querySelector('.modal-body .achievment-input-progress').value = response.progress
            achievmentModal.querySelector('.modal-body .achievment-input-progress').setAttribute('max', response.maxAmount != 0 ? response.maxAmount : "")
            var imageName = "images/" + response.achievedIconName;
            console.log(imageName);
            achievmentModal.querySelector('.modal-body .achievment-icon').src = imageName
        }
    }).done(function (msg) {
    }).fail(function (jqXHR, status) {
    });
}

function createAchievment() {
    achievmentModal.querySelector(".edit-achievment-id").setAttribute("asp-page-handler", -1)
    editAchievment();
}

function deleteAchievment() {
    var id = achievmentModal.querySelector(".edit-achievment-id").getAttribute("asp-page-handler");
    $.ajax({
        url: '/Achievments?handler=DeleteAchievment',
        type: 'get',
        data: {
            "id": id
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            //card.
        }
    }).done(function (msg) {
        console.log("response" + msg)
        window.location.reload();
        return false;
    }).fail(function (jqXHR, status) {
    });
}

function editAchievment() {
    var id = achievmentModal.querySelector(".edit-achievment-id").getAttribute("asp-page-handler");
    window.location.href = "/CreateEditAchievment?id=" + id;
}

function saveProgressChanges() {
    const inputProgress = achievmentModal.querySelector('.achievment-input-progress');
    var id = achievmentModal.querySelector(".edit-achievment-id").getAttribute("asp-page-handler");
    const card = document.getElementById("cardId:" + id);
    var newProgress = inputProgress.value
    $.ajax({
        url: '/Achievments?handler=SetProgress',
        type: 'get',
        data: {
            "id": id,
            "progress": newProgress
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
        }
    }).done(function (msg) {
        console.log("response" + msg)
        window.location.reload();
        return false;
    }).fail(function (jqXHR, status) {
    });
}



/*function editAchievment() {
    var yourUrl = '/Achievments?handler=Test';
    var xhr = new XMLHttpRequest();
    xhr.open("POST", yourUrl, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send();

    xhr.onload = function() {
        console.log("HELLO")
        console.log(this.responseText);
    var data = JSON.parse(this.responseText);
    console.log(data);
    }
}
    function editAchievment() {
        fetch('?handler=Test', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then((response) => response.json())
            .then((data) => {
                console.log('Success:', data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
}
    */
