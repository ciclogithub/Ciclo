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
        location.href = "Painel/Sair"
    }
}