$(document).ready(function () {

    $(".bg-menu").height($("#tg-main").height());

    $("#idpais").val(26);
    MudaPais(26);

    $("#incluir_btn_gravar").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            GravarNotificacoes();
        }
    });

});

function SelectedValues(field) {
    return $("input[name=" + field + "]:checked").map(
        function () { return this.value; }).get().join(",");
}

function GravarNotificacoes() {

    var txcategoria = SelectedValues("idcategoria");
    var txlocalidade = $("#form-modal #templocalidade").val();
    var txespecialidade = SelectedValues("idespecialidade");

    $.ajax({
        type: "POST",
        url: '/Aluno/Notificacoes/Gravar',
        data: { categoria: txcategoria, localidade: txlocalidade, especialidade: txespecialidade },
        dataType: "json",
        traditional: true,
        success: function (json) {
            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000,
            }).then((result) => {
                   
            })
        }
    });

}

function add(campo) {
    var val = $("#id" + campo + " option:selected").val();
    var text = $("#id" + campo + " option:selected").text();
    if (val != "") {
        if (!inArray(campo, val)) {
            $("#list" + campo).append("<li id=" + val + "><i class='glyphicon glyphicon-trash' onclick=remove('" + campo + "'," + val + ")></i><span>" + text + "</span></li>");
            if ($("#temp" + campo).val() == "") {
                $("#temp" + campo).val(val);
            } else {
                $("#temp" + campo).val($("#temp" + campo).val() + "," + val);
            }
        } else {
            alert("Já selecionado");
        }
    }
}

function remove(campo, i) {
    var arr = $("#temp" + campo).val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (arr[x] != i) { txt = txt + "," + arr[x]; } }
    $("#temp" + campo).val(txt.slice(1));
    $("#list" + campo + " li[id=" + i + "]").remove();
}

function inArray(campo, val) {
    arr = $("#temp" + campo).val().split(",");
    if ($.inArray(val, arr) < 0) { return false; } else { return true; }
}

function addLocalidade() {
    var valp = $("#idpais option:selected").val();
    var vale = $("#idestado option:selected").val();
    var valc = $("#idcidade option:selected").val();
    var textp = $("#idpais option:selected").text();
    var texte = $("#idestado option:selected").text();
    var textc = $("#idcidade option:selected").text();
    if (valp != "" || vale != "" || valc != "") {
        valor = valp + "_" + vale + "_" + valc;
        texto = textp;
        if (vale != "") { texto = texto + " / " + texte; }
        if (valc != "") { texto = texto + " / " + textc; }
        if (!inArray("localidade", valor)) {
            $("#listlocalidade").append("<li id=" + valor + "><i class='glyphicon glyphicon-trash' onclick=remove('localidade','" + valor + "')></i><span>" + texto + "</span></li>");
            if ($("#templocalidade").val() == "") {
                $("#templocalidade").val(valor);
            } else {
                $("#templocalidade").val($("#templocalidade").val() + "," + valor);
            }
        } else {
            alert("Já selecionado");
        }
    }
}