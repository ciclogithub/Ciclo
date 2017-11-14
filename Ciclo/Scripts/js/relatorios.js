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

});

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
            alert("Já selecionado")
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
        case 1: titulo = "Por Alunos"; titulo2 = "Por Alunos"; break;
        case 2: titulo = "Por Categorias"; titulo2 = "Por Alunos";  break;
        case 3: titulo = "Por Classificação (Aluno)"; titulo2 = "Por Alunos";  break;
        case 4: titulo = "Por Classificação (Curso)"; titulo2 = "Por Alunos";  break;
        case 5: titulo = "Por Cursos"; titulo2 = "Por Alunos";  break;
        case 6: titulo = "Por Especialiadades"; titulo2 = "Por Alunos";  break;
        case 7: titulo = "Por Locais"; titulo2 = "Total de alunos por curso";  break;
        case 8: titulo = "Por Instrutores"; titulo2 = "Por Alunos";  break;
        case 9: titulo = "Por Temas"; titulo2 = "Por Alunos";  break;            
    }

    if ($("#div_grafico").hasClass("hide")) {

        var tempCat = "";
        var arrCat = [];
        var arrSerie = [];

        $.ajax({
            url: "/Painel/Relatorios/Graficos",
            data: { ident: id },
            cache: false,
            type: "POST",
            success: function (data) {
                for (var x = 0; x < data.length; x++) {

                    if (tempCat != data[x].categoria) {
                        arrCat.push(data[x].categoria);
                        tempCat = data[x].categoria;
                    }

                    if (!$.inArray(data[x].serie, arrSerie)) {
                        arrSerie.push(data[x].serie);
                    }

                }

                Highcharts.chart('div_grafico', {
                    title: {
                        text: titulo
                    },
                    xAxis: {
                        categories: arrCat
                    },
                    labels: {
                        items: [{
                            html: titulo2,
                            style: {
                                left: '50px',
                                top: '18px',
                                color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
                            }
                        }]
                    },
                    series: [{
                        type: 'column',
                        name: 'Jane',
                        data: [3, 2, 1, 3, 4, 0 ,0 ,0 ,0 ,0 ,0]
                    }, {
                        type: 'column',
                        name: 'John',
                        data: [2, 3, 5, 7, 6, 0, 0, 0, 0, 0, 0]
                    }, {
                        type: 'column',
                        name: 'Joe',
                        data: [4, 3, 3, 9, 0, 0, 0, 0, 0, 0, 0]
                    }, {
                        type: 'spline',
                        name: 'Average',
                        data: [3, 2.67, 3, 6.33, 3.33, 0, 0, 0, 0, 0, 0],
                        marker: {
                            lineWidth: 2,
                            fillColor: 'white'
                        }
                    }, {
                        type: 'pie',
                        name: 'Total consumption',
                        data: [{
                            name: 'Jane',
                            y: 13,
                        }, {
                            name: 'John',
                            y: 23,
                        }, {
                            name: 'Joe',
                            y: 19,
                        }],
                        center: [100, 80],
                        size: 100,
                        showInLegend: false,
                        dataLabels: {
                            enabled: false
                        }
                    }]
                });
                $("#div_grafico").removeClass("hide");

            },
            error: function (reponse) {
                $("#div_grafico").html("Não foi possível carregar o gráfico");
            }
        });

    }
}