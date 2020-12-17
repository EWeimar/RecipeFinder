var ingredientTemplate = document.getElementById('ingredientTemplate').innerHTML;
var imageTemplate = document.getElementById('imageTemplate').innerHTML;
var ingredientLines = [];
var images = [];

function addIngredientLine() {
    
    var ingredient_name = $('#ingredient_add_name').val();
    var ingredient_amount = $('#ingredient_add_amount').val();
    var ingredient_measure_unit = $('#ingredient_add_measure_unit').children("option:selected").val();
    var ingredient_measure_unit_name = $('#ingredient_add_measure_unit').children("option:selected").text();
    

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

    ingredientLines.push({ name: ingredient_name, amount: ingredient_amount, unit: parseInt(ingredient_measure_unit), identifier: identifier, unit_name: ingredient_measure_unit_name });
    console.log(ingredientLines);

    $('#ingredient_add_name').val("");
    $('#ingredient_add_amount').val("");

    render();
}

function addImage() {
    var filename = $('#image_add_name').val();
    var identifier = Math.floor(Math.random() * Math.floor(Math.random() * Date.now()));

    images.push({ filename: filename, identifier: identifier });

    $('#image_add_name').val();
    console.log(images);

    $('#image_add_name').val("");

    renderImage();
}

function renderImage() {
    $('#recipe_images').html("");
    if (images.length == 1) {
        $('#recipe_images').html("");
    }

    for (let i = 0; i < images.length; i++) {
        var renderedImages = Mustache.render(imageTemplate, { index: i, filename: images[i].filename, identifier: images[i].identifier });
        console.log("line: " + images[i].identifier);

        $('#recipe_images').append(renderedImages);
    }
}

function render() {
    console.log("Render method called!");
    $('#ingredient_lines').html("");
    

    for (let i = 0; i < ingredientLines.length; i++) {
        if (ingredientLines.length == 0) {
            $('#ingredient_lines').html("<div class=\"p-5 text-center text-muted\">Please add some ingredients</div>");
        }
        if (ingredientLines.length == 1) {
            $('#ingredient_lines').html("");
        }

        var renderedIngredientLine = Mustache.render(ingredientTemplate, { index: i, ingredient_name: ingredientLines[i].name,
            ingredient_amount: ingredientLines[i].amount, ingredient_unit: ingredientLines[i].unit, identifier: ingredientLines[i].identifier,
            ingredient_unit_name: ingredientLines[i].unit_name
        });

        console.log("line: " + ingredientLines[i].identifier);
        
        $('#ingredient_lines').append(renderedIngredientLine);
    }
}

function removeIngredientLine(index, identifier) {
    console.log("Removed ingredient: " + identifier);
    console.log(ingredientLines);
    ingredientLines.splice(index, 1);
    $("#ingredient_line_element_" + identifier).remove();
    render();
}

function removeImage(index, identifier) {
    console.log("Removed image: " + identifier);
    console.log(images);
    images.splice(index, 1);
    $("#image_element_" + identifier).remove();
    renderImage();
}

$('#add_image_btn').click(function () {
    addImage();

});


$('#add_ingredient_btn').click(function () {
    addIngredientLine();
    
});

$(document).ready(function () {
    render();
    renderImage();
});

var form = $('#creat_recipe_form');

form.submit(function (e) {
    if (ingredientLines.length > 0) {
        form.submit();
    } else {
        alert("Please add some ingredients!");
        e.preventDefault();
    }
});