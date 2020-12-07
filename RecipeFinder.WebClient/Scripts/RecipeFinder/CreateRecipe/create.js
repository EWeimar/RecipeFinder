var template = document.getElementById('template').innerHTML;
var lines = [];

function addIngredientLine() {
    
    var ingredient_name = $('#ingredient_add_name').val();
    var ingredient_amount = $('#ingredient_add_amount').val();
    var ingredient_measure_unit = $('#ingredient_add_measure_unit').children("option:selected").val();
    

    if (ingredient_name == "") {
        alert("Please enter an ingredient!");
        return;
    }

    if (parseInt(ingredient_amount) < 0 || Number.isNaN(parseInt(ingredient_amount))) {
        alert("Please enter a number equal or greater than 0!");
        return; 
    }
    
    if (parseInt(ingredient_measure_unit) < 0 || Number.isNaN(parseInt(ingredient_measure_unit))) {
        alert("Please select a valid unit of measure!");
        return;
    }

    var identifier = Math.floor(Math.random() * Math.floor(Math.random() * Date.now()))

    lines.push({ name: ingredient_name, amount: ingredient_amount, unit: parseInt(ingredient_measure_unit), identifier: identifier });
    console.log(lines);

    $('#ingredient_add_name').val("");
    $('#ingredient_add_amount').val("");

    render();
}

function render() {
    console.log("Render method called!");
    $('#ingredient_lines').html("");
    for (let i = 0; i < lines.length; i++) {
        if (lines.length == 0) {
            $('#ingredient_lines').html("<div class=\"p-5 text-center text-muted\">Please add some ingredients</div>");
        }
        if (lines.length == 1) {
            $('#ingredient_lines').html("");
        }


        var renderedIngredientLine = Mustache.render(template, { index: i, ingredient_name: lines[i].name, ingredient_amount: lines[i].amount, ingredient_unit: lines[i].unit, identifier: lines[i].identifier });
        console.log("line: " + lines[i].identifier);
        //if (!$('#ingredient_line_element_' + lines[i].identifier).length > 0) {
            $('#ingredient_lines').append(renderedIngredientLine);
        //}
    }

}

function removeIngredientLine(index, identifier) {
    console.log("Removed ingredient: " + identifier);
    console.log(lines);
    lines.splice(index, 1);
    $("#ingredient_line_element_" + identifier).remove();
    render();
}

$('#add_ingredient_btn').click(function () {
    addIngredientLine();
    
});

$(document).ready(function () {
    render();
});