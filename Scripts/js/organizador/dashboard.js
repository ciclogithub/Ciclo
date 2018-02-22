$(document).ready(function () {

    cont = $("#cont_inscricoes").val();

    if (cont > 0) {
        swal({
            title: 'Existem ' + cont + ' novas solicitações de inscrições para cursos',
            type: 'info',
            showCancelButton: true,
            confirmButtonText: 'Ver todas',
            cancelButtonText: 'Fechar',
        }).then((result) => {
            if (result.value) {
                location.href = "Painel/Inscricoes";
            }
        });
    }  

});