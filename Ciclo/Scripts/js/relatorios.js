$(document).ready(function () {

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            $('#form-modal').submit();
        };
    });

});

function add(campo) {
    var val = $("#id" + campo + " option:selected").val();
    var text = $("#id" + campo + " option:selected").text();
    if (val != "") {
        if (!inArray(campo, val)) {
            $("#list" + campo).append("<li id=" + val + "><i class='glyphicon glyphicon-trash' onclick=remove('" + campo + "'," + val + ")></i><span>" + text + "</span></li>");
            $("#temp" + campo).val($("#temp" + campo).val() + "," + val);
        } else {
            alert("Já selecionado")
        }
    }
}

function remove(campo, i) {
    var arr = $("#temp" + campo).val().slice(1).split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (arr[x] != i) { txt = txt + "," + arr[x]; } }
    $("#temp" + campo).val(txt);    
    $("#list" + campo + " li[id=" + i + "]").remove();
}

function inArray(campo, val) {
    arr = $("#temp" + campo).val().split(",");
    if ($.inArray(val, arr) < 0) { return false; } else { return true; }
}

function buscaAvancada() {
    if ($("#busca_avancada").hasClass("hide")) {
        $("#busca_avancada").removeClass("hide");
    } else {
        $("#busca_avancada").addClass("hide");
    }
}