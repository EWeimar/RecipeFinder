
var template = document.getElementById('template').innerHTML;
var lines = [];

function render() {
    if (lines.length == 0) {
        $('#ingredient_lines').html("<div class=\"p-5 text-center text-muted\">Please add some ingredients</div>");
    }
    if (lines.length == 1) {
        $('#ingredient_lines').html("");
    }

    for (let i = 0; i < lines.length; i++) {
        var renderedIngredientLine = Mustache.render(template, { index: i, text: lines[i].text, amount: lines[i].amount, unit: lines[i].unit });

        if (!$("#ingredient_form_" + i).length > 0) {
            $('#ingredient_lines').append(renderedIngredientLine);
        }
    }
}

function addIngredientLine() {
    lines.push({ text: "", unit: "", amount: "" });
    render();
}

function removeIngredientLine(index) {
    lines.splice(index, 1);
    $("#ingredient_form_" + index).remove();
    render();
}

$('#add_ingredient_btn').click(function () {
    addIngredientLine();
});

$(document).ready(function () {
    render();
});