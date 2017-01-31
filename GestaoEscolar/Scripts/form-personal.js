$.fn.radio = function (elemShow) {
    this.change(function () {
        if (this.value === 'sim') {
            $(elemShow).show();

        }
        if (this.value === 'nao') {
            $(elemShow).hide();
        }
    });
};

$('.ProblemaSaudeAluno').radio('div[id = "DivDescProblemaSaudeAluno"]');
$('.DefFisicaAluno').radio('div[id = "DivDescDefFisicaAluno"]');
$('.DefVisualAluno').radio('div[id = "DivDescDefVisualAluno"]');
$('.DefAuditivaAluno').radio('div[id = "DivDescDefAuditivaAluno"]');
$('.AltaHabilidade').radio('div[id = "DivDescAltaHabilidade"]');

