$(function () {
    $("#incluir_btn_alterar_senha").click(function () {
        $('#form-modal_senha').validationEngine('attach');
        if ($('#form-modal_senha').validationEngine('validate')) {
            AlterarSenha();
        }
    });
});

function AlterarSenha() {

    var txsenhaatual = $("#form-modal_senha #txsenhaatual").val();
    var txnovasenha = $("#form-modal_senha #txnovasenha").val();

    $.ajax({
        type: "POST",
        url: '/Aluno/AlterarSenha/AlterarSenha',
        data: { senha: txsenhaatual, novasenha: txnovasenha },
        dataType: "json",
        traditional: true,
        success: function (json) {
            json = jQuery.parseJSON(json);
            if (json.retorno == "OK") {
                swal({
                    title: 'Operação realizada com sucesso!',
                    type: 'success',
                    confirmButtonText: 'Fechar',
                    timer: 3000,
                }).then((result) => {
                    if (result.dismiss === 'timer') {
                        location.href = "AlterarSenha";
                    }
                    if (result.value) {
                        location.href = "AlterarSenha";
                    }
                })
            } else {
                swal({ title: json.retorno, type: "error", timer: 3000 });
            }
        }
    });

}