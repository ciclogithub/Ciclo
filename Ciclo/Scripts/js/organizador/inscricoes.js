$(document).ready(function () {

    $("#pesquisa_inscricao").click(function () {
        InscricaoPesquisar();
    });

});

function InscricaoPesquisar() {
    window.location = "/Painel/Inscricoes/?filtro=" + $("#filtro_pesquisa").val() + "&status=" + $("#filtro_status").val();
}

function InscricaoRecusar() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        swal({
            title: 'Confirma o cancelamento de inscrição do(s) registro(s)',
            type: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: '/Painel/Inscricoes/Cancelamento',
                    data: { id: ids },
                    dataType: "json",
                    traditional: true,
                    success: function (json) {
                        swal({
                            title: 'Operação realizada com sucesso!',
                            type: 'success',
                            confirmButtonText: 'Fechar',
                            timer: 3000
                        }).then((result) => {
                            if (result.dismiss == 'timer') {
                                InscricaoPesquisar();
                            }
                            if (result.value) {
                                InscricaoPesquisar()
                            }
                        });

                    }
                });
            }
        });
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function InscricaoConfirmar() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        swal({
            title: 'Confirma a inscrição do(s) registro(s)',
            type: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: '/Painel/Inscricoes/Confirmacao',
                    data: { id: ids },
                    dataType: "json",
                    traditional: true,
                    success: function (json) {
                        swal({
                            title: 'Operação realizada com sucesso!',
                            type: 'success',
                            confirmButtonText: 'Fechar',
                            timer: 3000
                        }).then((result) => {
                            if (result.dismiss == 'timer') {
                                InscricaoPesquisar();
                            }
                            if (result.value) {
                                InscricaoPesquisar()
                            }
                        });

                    }
                });
            }
        });
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}