$(document).ready(function () {
    CarregarListaModelos($(".form-mail #idtipo").val(), 0);    
});

function CarregarRecusa() {
    var val = $(".form-mail #idmodelo").val();
    if (val > 0) {
        $.ajax({
            url: "/Painel/Painel/ModelosEmailCarregar",
            data: { id: val },
            cache: false,
            type: "POST",
            success: function (data) {
                $("#mensagem").val(data.txmensagem);
            },
            error: function (reponse) {
                swal({ title: "Não foi possível carregar o modelo", type: "error", timer: 3000 })
            }
        });
    } else {
        swal({ title: "Informe o modelo", type: "error", timer: 3000 });
    }
}

function GravarRecusa() {
    var val = $(".form-mail #idmodelo").val();
    var msg = $(".form-mail #mensagem").val();

    if (msg == "") {
        swal({ title: "Informe a mensagem", type: "error", timer: 3000 });
    } else {
        var txt = "";
        var btn = "Gravar"
        if (val > 0) {
            txt = $(".form-mail #idmodelo option:selected").text();
            btn = "Atualizar"
        }
        swal({
            title: 'Informe o titulo do modelo',
            input: 'text',
            inputValue: txt,
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonText: btn,
            showLoaderOnConfirm: true,
            inputPlaceholder: 'Título',
            inputValidator: (value) => {
                if (!value) {
                    return 'Preencha o título corretamente';
                } else {
                    if (value.length > 100) {
                        return 'Preencha o título com até 100 caracteres';
                    }
                }
            },
            allowOutsideClick: false
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: "/Painel/Inscricoes/GravarModelo",
                    data: { id: val, tipo: 1, titulo: result.value, mensagem: msg },
                    traditional: true,
                    async: false,
                    success: function (data) {
                        swal({ title: "Modelo cadastrado com sucesso", type: "success", timer: 3000 });
                        CarregarListaModelos(1, data); 
                    }
                });
            }
        })
    }
}

function CarregarListaModelos(tipo, val) {
    $.ajax({
        url: "/Painel/Painel/ModelosEmail",
        data: { tipo: tipo },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            temp += "<select id='idmodelo' name='idmodelo' class='form-control no-js'><option value='0'>-- Selecione o Modelo --</option>";
            for (var x = 0; x < data.length; x++) {
                temp += "<option value=" + data[x].idmodelo;
                if (data[x].idmodelo == val) { temp += " selected "; }
                temp += ">" + data[x].txmodelo + "</option>";
            }
            temp += "</select>";
            $("#dv_modelo").html(temp);
            $('select#idmodelo').select2();
        },
        error: function (reponse) {
            $("#dv_modelo").html("Não foi possível carregar a lista de modelos");
        }
    });
}

function RecusarConfirmar() {
    ids = $(".form-mail #ids").val();
    msg = $(".form-mail #mensagem").val();
    if (ids == "") {
        swal({ title: "Nenhuma inscrição foi informada", type: "error", timer: 3000 });
    } else {
        $.ajax({
            type: "POST",
            url: "/Painel/Inscricoes/RecusarConcluir",
            data: { id: ids, mensagem: msg },
            dataType: "json",
            traditional: true,
            async: false,
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
}


//function InscricaoConfirmar() {

//    ids = "";
//    $("input[name='ident']").each(function () {
//        if ($(this).is(":checked")) {
//            ids = ids + "," + $(this).val();
//        }
//    });

//    var ids = ids.substring(1);

//    if (ids != "") {
//        swal({
//            title: 'Confirma a inscrição do(s) registro(s)',
//            type: 'question',
//            showCancelButton: true,
//            confirmButtonText: 'Sim',
//            cancelButtonText: 'Não'
//        }).then((result) => {
//            if (result.value) {
//                $.ajax({
//                    type: "POST",
//                    url: '/Painel/Inscricoes/Confirmacao',
//                    data: { id: ids },
//                    dataType: "json",
//                    traditional: true,
//                    async: false,
//                    success: function (json) {
//                        swal({
//                            title: 'Operação realizada com sucesso!',
//                            type: 'success',
//                            confirmButtonText: 'Fechar',
//                            timer: 3000
//                        }).then((result) => {
//                            if (result.dismiss == 'timer') {
//                                InscricaoPesquisar();
//                            }
//                            if (result.value) {
//                                InscricaoPesquisar()
//                            }
//                        });

//                    }
//                });
//            }
//        });
//    } else {
//        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
//    }
//}
