(function($){
    $.fn.validationEngineLanguage = function(){};
    $.validationEngineLanguage = {
        newLang: function(){
            $.validationEngineLanguage.allRules = {
                "required": {
                    "regex": "none",
                    "alertText": "Obrigatório",
                    "alertTextCheckboxMultiple": "Selecione uma opção",
                    "alertTextCheckboxe": "Selecione uma opção",
                    "alertTextDateRange": "Ambas as datas do intervalo são obrigatórias"
                },
				"requiredIf": {
					"regex": "none",
                    "alertText": "Obrigatório",
				},
                "requiredInFunction": { 
                    "func": function(field, rules, i, options){
                        return (field.val() == "test") ? true : false;
                    },
                    "alertText": "Field must equal test"
                },
                "dateRange": {
                    "regex": "none",
                    "alertText": "Intervalo de datas inválido"
                },
                "dateTimeRange": {
                    "regex": "none",
                    "alertText": "Intervalo de data e hora inválido"
                },
                "minSize": {
                    "regex": "none",
                    "alertText": "Mínimo de ",
                    "alertText2": " caractere(s)"
                },
                "maxSize": {
                    "regex": "none",
                    "alertText": "Máximo de ",
                    "alertText2": " caractere(s)"
                },
				"groupRequired": {
                    "regex": "none",
                    "alertText": "Você deve preencher um dos seguintes campos"
                },
                "min": {
                    "regex": "none",
                    "alertText": "Valor mínimo é " 
                },
                "max": {
                    "regex": "none",
                    "alertText": "Valor máximo é "
                },
                "past": {
                    "regex": "none",
                    "alertText": "Data anterior a "
                },
                "future": {
                    "regex": "none",
                    "alertText": "Data posterior a "
                },	
                "maxCheckbox": {
                    "regex": "none",
                    "alertText": "Máximo de ",
                    "alertText2": " opções permitidas" 
                },
                "minCheckbox": {
                    "regex": "none",
                    "alertText": "Favor selecionar ",
                    "alertText2": " opção(ões)" 
                },
                "equals": {
                    "regex": "none",
                    "alertText": "Os campos devem ser iguais" 
                },
                "creditCard": {
                    "regex": "none",
                    "alertText": "Número de cartão de crédito inválido"
                },
				"time": {
                    "regex": /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/,
                    "alertText": "Horário inválido"
                },
                "phone": {
                    "regex": /^([\+][0-9]{1,3}[ \.\-])?([\(]{1}[0-9]{2,6}[\)])?([0-9 \.\-\/]{3,20})((x|ext|extension)[ ]?[0-9]{1,4})?$/,
                    "alertText": "Número de telefone inválido"
                },
                "email": {
                    "regex": /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i,
                    "alertText": "E-mail inválido"
                },
                "integer": {
                    "regex": /^[\-\+]?\d+$/,
                    "alertText": "Número inteiro inválido"
                },
                "number": {
                    "regex": /^[\-\+]?((([0-9]{1,3})([,][0-9]{3})*)|([0-9]+))?([\.]([0-9]+))?$/,
                    "alertText": "Número decimal inválido"
                },
                "date": {
                    "regex": /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/,
                    "alertText": "Data inválida, deve ser no formato DD/MM/AAAA"
                },
                "ipv4": {
                    "regex": /^((([01]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))[.]){3}(([0-1]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))$/,
                    "alertText": "Endereço IP inválido"
                },
                "url": {
                    "regex": /[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/,
                    "alertText": "URL inválida" 
                },
                "onlyNumber": {
                    "regex": /^[0-9\ ]+$/,
                    "alertText": "Apenas números"
                },
                "onlyLetterSp": {
                    "regex": /^[a-zA-Z\ \']+$/,
                    "alertText": "Apenas letras"
                },
                "onlyLetterNumber": {
                    "regex": /^[0-9a-zA-Z]+$/,
                    "alertText": "Somente letras e números"
                },
                "real": {
                	// Brazilian (Real - R$) money format
                	"regex": /^([1-9]{1}[\d]{0,2}(\.[\d]{3})*(\,[\d]{0,2})?|[1-9]{1}[\d]{0,}(\,[\d]{0,2})?|0(\,[\d]{0,2})?|(\,[\d]{1,2})?)$/,
                    "alertText": "Número decimal inválido"
                },
				"captcha": {
					"regex": "none",
					"alertText": "Código de verificação deve possuir 8 caracteres",
					"alertText2": "Código de verificação incorreto",
				},
				"existValue": {
					"regex": "none",
					"alertText": "Já existe outro cadastro com esta descrição"
				},
				"notEquals": {
                    "regex": "none",
                    "alertText": "Os campos não podem ser iguais"
                },
				"cpf": {
                    "regex": "none",
                    "alertText": "CPF inválido"
                },
				"cnpj": {
                    "regex": "none",
                    "alertText": "CNPJ inválido"
                },
            };
            
        }
    };

    $.validationEngineLanguage.newLang();
    
})(jQuery);

