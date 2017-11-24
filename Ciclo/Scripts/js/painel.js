$(function () {
    $('.table tr th[scope=row]').click(function (event) {
        if (!$(event.target).is('input')) {
            var obj = $(this).find('input');
            obj.prop('checked', !obj.is(':checked'));
        }
    });

    $('#selectall').click(function () {
        val = $(this).prop('checked')
        $('.table tr th[scope=row]').find('input').each(function (index) {
            $(this).prop('checked', val);
        });
    })
});



$('.counter-count').each(function () {
    $(this).prop('Counter',0).animate({
        Counter: $(this).text()
    }, {
        duration: 1000,
        easing: 'swing',
        step: function (now) {
            $(this).text(Math.ceil(now));
        }
    });
});

function confirmaSair() {
    if (confirm("Confirma a saída do sistema?")) {
        location.href = "/Painel/Painel/Sair"
    }
}

var Aguarde;
Aguarde = Aguarde || (function () {
    var pleaseWaitDiv = $('<div class="modal fade bs-example-modal-sm" id="modalLoading" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true"><div class="modal-dialog modal-sm Aguarde-loading"><p>Aguarde ...</p><img src="data:image/gif;base64,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" /></div></div>');
    return {
        show: function () {
            pleaseWaitDiv.modal();
        },
        hide: function () {
            pleaseWaitDiv.modal('hide');
        },

    };
})();

function AguardeDiv() {
    var pleaseWaitDiv = '<div class="Aguarde-fade"><BR><BR><BR><p>Carregando ...</p><img src="data:image/gif;base64,R0lGODlhNgA3AIAAAM7Ozv///yH/C05FVFNDQVBFMi4wAwEAAAAh+QQJCgABACwHAAUALQAwAAACfoyPmcCtDyNsFMiLT625z+14ogYy41map5haq9e+xvbRclmHDyXhN5fj/YQ7kCxgKyZXy4XxSAy6jtSq9YrNarfcriLK5TWzLVW47C1PtzQwW+eNy+f0uv2OdTPhSj4KKEU1hjDIAthn9lJIkji0hpfnF7f4ptdFqYWZKZlRAAAh+QQJCgABACwHAAYALQAvAAACfYyPmcCtDyNsFMiLUXW5x814ogJa4xmU6KmS1bq87iZRcFqaM4fmemxb+SayXo4YNLYeSSXo1llCMbSp9YrNarfcGq9rq3J9ITC5ixuaw99xGw2Py+f0ut3ahOV3ZX2R+efUB7g3Ikb4Zhi4g5d4N1WIdhg3CVcpuWjpiFEAACH5BAkKAAEALAcABwAvAC4AAAJ9jI+pwKAPozStzYtTZbnf7XjiA46mZp1qVn6pCnJSe8ZhRJv2lI/7vFn9gDJYbOULIpfMpvMJjRorUtwxQJXahs/tFertNcNK8Bb7MnOrlC8bIX7L5/S6PVkUpq3Z6Z5UVhMIEedRCDeI9weY53c312d3WDWplShXadkoUgAAIfkECQoAAQAsBwAHAC4ALgAAAnqMj6nAoA+jDK3Ni1Fluc/teOKyjaZmneoaVazhfuUKyrFaSzk+t/2583Fet5fxiEwql8ym85krKkHU0LIaTGJ/2m3qSqV8wVko94lOq9dmafuccHu2wrGIDil38A99hk8i94elY3dHmFZ1JCjGeOhIZvg2tObnBClRAAAh+QQJCgABACwHAAcALQAwAAACfYyPqcCgD6NsTdq7qMPc6t2FGSWWCWmm6lStBhp9rhxrs13DKQ3xO54DrXSuovGITCqXzKaTxXgGSD7lp5q8ApdaYtbmtbak5LL5jE6Xsafxj/3alrTBqIrek4vwDziH3+c2Ihjn93cVGBbSJcTEKPX4FOk02VRpibimh1EAACH5BAkKAAEALAcABwAtAC8AAAJ5jI+pwKAPo2xN2ruow9zq3YUZJZYJaabqVK0GGn2uHGuzXcMpDfE7ngOtdK6i8YhMKpfMppPFeAZIPuWnmrwCl1pi1ua1tqTksvmMRmCbWnJXWAq/uqr1lP6Tv+tbNd5kt8cXFaTXB3g4Zye2+NWI1OZ2VRYpKWdRAAAh+QQJCgABACwFAAcALwAuAAACe4yPqQYNC6OEzs2LU2253/144gKOpmad6jpVLJO2G1vK7lpL+blTM/6zhWixl/GITCqXTF6wiepBQbeli1pEYrHMLbXrfWrDVeWNC1VI0+y2+w2PrKdfIEdYHs0PaGdeXeeXBbiHURjQJ2goFvcx2HZoxcgWaTZJ+YhRAAAh+QQJCgABACwGAAcALgAuAAACfYyPqQENC6Nczs2LUW25z/14ogKO1CZVJgmGa9darxrF3FpCtjvmZzxD6WxB2q/1YshqyKTH54xKp9Sq9aoRYllQqwqI3Rm94tu1bCaXt8pblw2Py+f0ul06LvKG+Z6Wv4Tzd5Rm8pZw+DR4hBd4p7Ynl7g1GbYIV2lZ2FEAADs=" /><BR><BR><BR></div>';
    return pleaseWaitDiv;
};

var ajax_conteudo;
function Conteudo(url) {

    Aguarde.show();

    if (ajax_conteudo && ajax_conteudo.readyState != 4) {
        ajax_conteudo.abort();
    }

    ajax_conteudo = $.ajax({
        type: "POST",
        url: url,
        dataType: "html",
        traditional: true,
        success: function (msg) {
            Aguarde.hide();
            $("main.body-content").html(msg);
        }
    });
}

function ConteudoPaginacao(url, pagina) {

    Aguarde.show();

    if (pagina == null)
        pagina = 1;

    if (ajax_conteudo && ajax_conteudo.readyState != 4) {
        ajax_conteudo.abort();
    }

    ajax_conteudo = $.ajax({
        type: "POST",
        url: url,
        data: { pagina: pagina },
        dataType: "html",
        traditional: true,
        success: function (msg) {
            Aguarde.hide();
            $("main.body-content").html(msg);
        }
    });
}

function Modal(url, id, titulo, func) {
    if (ajax_conteudo && ajax_conteudo.readyState != 4) {
        ajax_conteudo.abort();
    }

    $("#modal1.modal .modal-title").text(titulo);
    $("#modal1.modal .modal-body").empty();

    ajax_conteudo = $.ajax({
        type: "POST",
        url: url,
        data: { id: id },
        dataType: "html",
        traditional: true,
        success: function (msg) {           
            $("#modal1.modal .modal-body").html(msg);
            $('#modal1.modal').modal('show');
            
            if (func != "") {
                $("#modal1.modal").on("shown.bs.modal", function () {
                    var funcao = func + "(" + id + ")"
                    eval(funcao);
                    $(this).off('shown.bs.modal');
                });

            }

        }
    });

}

function Modal2(url, id, titulo, func) {
    if (ajax_conteudo && ajax_conteudo.readyState != 4) {
        ajax_conteudo.abort();
    }

    $("#modal2.modal .modal-title").text(titulo);
    $("#modal2.modal .modal-body").empty();

    ajax_conteudo = $.ajax({
        type: "POST",
        url: url,
        data: { id: id },
        dataType: "html",
        traditional: true,
        success: function (msg) {
            $("#modal2.modal .modal-body").html(msg);
            $('#modal2.modal').modal('show');

            if (func != "") {
                $("#modal2.modal").on("hidden.bs.modal", function () {
                    var funcao = func + "(" + id + ")"
                    eval(funcao);
                    $(this).off('hidden.bs.modal');
                });

            }

        }
    });

}

function Alert(texto, alert, tag, timeout) {

    // alert => success, info, warning, danger
    if (tag == undefined || tag == null || tag == '')
        tag = "body";

    var txt = '<div class="alert alert-' + alert;

    if (tag == "body")
        txt += ' alert-position';

    txt += ' alert-dismissible fade in" role="alert"> <button type="button" class="close" data-dismiss="alert" aria-label="Fechar"><span aria-hidden="true">×</span></button> <p class="pull-left"> ' + texto + ' </p></div>';

    if (timeout != undefined && timeout != null && timeout != '' && timeout != '0' && timeout != 0)
        setTimeout(function () {
            $(".alert").alert('close');
        }, timeout);

    $(tag).append(txt);
}


function lista(id) {
    $('body').html('<form action="Painel/Listas/Relatorio" id="form_red" method="post" style="display:none;"><input type= "hidden" id="tprelatorio" name="tprelatorio" value="' + id + '" /></form >');
    $("#form_red").submit();
}

function pagination(c) {
    var n = $("#namepage").val();
    var p = $("#page").val();
    var t = $("#totalpage").val();
    if (c === -1) {
        c = parseInt(p) - 1;
        if (c <= 0) { c = 1; }
        window.location = "/Painel/" + n + "/?pagina=" + c + "&filtro=" + $("#filtro_pesquisa").val();
    } else {
        if (c === 0) {
            c = parseInt(p) + 1;
            if (c > t) { c = t; }
            window.location = "/Painel/" + n + "/?pagina=" + c + "&filtro=" + $("#filtro_pesquisa").val();
        } else {
            window.location = "/Painel/" + n + "/?pagina=" + c + "&filtro=" + $("#filtro_pesquisa").val();
        }
    }
}