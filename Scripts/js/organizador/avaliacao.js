$(document).ready(function () {

    $('[id^=resp_]').barrating({
        theme: 'fontawesome-stars'
    });

    $("#btn_cadastro_avaliacao").click(function () {
        $('#form_cadastro_avaliacao').validationEngine('attach');
        if ($('#form_cadastro_avaliacao').validationEngine('validate')) {
            IncluirAvaliacao();
        }
    });

});

function IncluirAvaliacao() {

    var idcursoaluno = $("#idcursoaluno").val();
    var resp_1 = $("#resp_1").val();
    var resp_2 = $("#resp_2").val();
    var resp_3 = $("#resp_3").val();
    var resp_4 = $("#resp_4").val();
    var resp_5 = $("#resp_5").val();
    var txdescritivo = $("#txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Avaliacao/IncluirConcluir',
        data: { id: idcursoaluno, nota1: resp_1, nota2: resp_2, nota3: resp_3, nota4: resp_4, nota5: resp_5, obs: txdescritivo },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Avaliação enviada com sucesso!");
            window.location.reload();
        }
    });

}