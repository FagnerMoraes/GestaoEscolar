//Inserir caracteres em Uppercase

$(function () {
    $('.upper').keyup(function () {
        this.value = this.value.toLocaleUpperCase();
    });
});


//-----------------------Mascara Escola-----------------------
jQuery(function ($) {
    $("#Cnpj").mask("99.999.999/9999-99");
    $("#CepEscola").mask("99999-999");
    $("#TelefoneEscola").mask("(99)99999-9999");
    $("#FaxEscola").mask("(99)99999-9999");
});
//-----------------------Fim Mascara Escola-------------------


