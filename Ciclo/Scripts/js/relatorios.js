$(document).ready(function () {

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            $('#form-modal').submit();
        };
    });

    $("#exportar").click(function () {
        $("#table2excel").table2excel({
        exclude: ".noExl",
        name: "Relatório",
        filename: "Relatório - " + $("ol li.tg-active").html()
        });
    });

    $('select').not('.no-js').select2();

});

function add(campo) {
    var val = $("#id" + campo + " option:selected").val();
    var text = $("#id" + campo + " option:selected").text();
    if (val !== "") {
        if (!inArray(campo, val)) {
            $("#list" + campo).append("<li id=" + val + "><i class='glyphicon glyphicon-trash' onclick=remove('" + campo + "'," + val + ")></i><span>" + text + "</span></li>");
            if ($("#temp" + campo).val() === "") {
                $("#temp" + campo).val(val);
            } else {
                $("#temp" + campo).val($("#temp" + campo).val() + "," + val);
            }
        } else {
            alert("Já selecionado")
        }
    }
}

function remove(campo, i) {
    var arr = $("#temp" + campo).val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (arr[x] !== i) { txt = txt + "," + arr[x]; } }
    $("#temp" + campo).val(txt.slice(1));    
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

function abreGrafico(id) {
    var titulo = "";
    var titulo2 = "";

    switch (id) {
        case 1: titulo = "Total de alunos por período"; titulo2 = "Alunos"; break;
        case 2: titulo = "Total de alunos por categoria"; titulo2 = "Alunos";  break;
        case 3: titulo = "Total de alunos por classificação"; titulo2 = "Alunos";  break;
        case 4: titulo = "Total de alunos por classificação"; titulo2 = "Alunos";  break;
        case 5: titulo = "Total de alunos por curso"; titulo2 = "Alunos";  break;
        case 6: titulo = "Total de alunos por mercados"; titulo2 = "Alunos";  break;
        case 7: titulo = "Total de alunos por local"; titulo2 = "Alunos";  break;
        case 8: titulo = "Total de alunos por instrutor"; titulo2 = "Alunos";  break;
        case 9: titulo = "Total de alunos por tema"; titulo2 = "Alunos";  break;            
    }

    if ($("#div_grafico").hasClass("hide")) {

        $("#div_grafico").removeClass("hide");
        $("#div_grafico").html("Carregando gráfico ...");

        var tempCat = "";
        var arrCat = [];
        var arrSerie = [];
        var arrValues = {};
        var tempserie = "";

        $.ajax({
            url: "/Painel/Relatorios/Graficos",
            data: $("#form-filtro").serialize(),
            cache: false,
            type: "POST",
            success: function (data) {
                for (var x = 0; x < data.length; x++) {

                    if (tempCat !== data[x].categoria) {
                        arrCat.push(data[x].categoria);
                        tempCat = data[x].categoria;
                    }

                    if ($.inArray(data[x].serie, arrSerie) === -1) {
                        arrSerie.push(data[x].serie);
                        arrValues[data[x].serie] = {};
                    }

                    arrValues[data[x].serie][data[x].categoria] = data[x].valor;
                }
                arrSerie.sort();
                
                chart = new Highcharts.chart('div_grafico', {
                    credits: false,
                    title: {
                        text: titulo
                    },
                    xAxis: {
                        categories: arrCat
                    },
                    yAxis: {
                        title: {
                            text: titulo2
                        }
                    },

                });

                var valor = "";
                $.each(arrSerie, function (key1, value1) {

                    valor = [];

                    $.each(arrCat, function (key2, value2) {
                        if (arrValues[value1][value2] === undefined) {
                            valor.push(0);
                        } else {
                            valor.push(arrValues[value1][value2]);
                        }
                    });

                    chart.addSeries({
                        type: 'column',
                        name: value1,
                        data: valor,
                    }, false);

                });
                chart.redraw();

            },
            error: function (reponse) {
                $("#div_grafico").html("Não foi possível carregar o gráfico");
            }
        });

    }
}