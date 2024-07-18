function showAlert(title, text, type) {
    Swal.fire({
        title,
        text,
        icon: type === 0 ? "error" : "success",
        showCancelButton: false,
        confirmButtonText: "Ok",
        closeOnConfirm: true,
    }).then((result) => {
        if (result.isConfirmed) {

        }
    });
    return false;
}

function showAlertAndReload(title, text, type) {
    Swal.fire({
        title,
        text,
        icon: type === 0 ? "error" : "success",
        showCancelButton: false,
        confirmButtonText: "Ok",
        closeOnConfirm: true,
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.reload();
        }
    });
    return false;
}

function confirmDelete(title, text, callback) {
    Swal.fire({
        title,
        text,
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        icon: "question"
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            if (callback && typeof callback === "function") {
                setTimeout(function () {
                    callback();
                }, 200);
            }
        }
    });
}