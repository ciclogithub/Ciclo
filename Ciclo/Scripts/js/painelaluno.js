$(function () {

});


function confirmaSair() {
    swal({
        title: 'Deseja realmente sair do sistema',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            location.href = "/Aluno/Aluno/Sair"
        }
    })
}

