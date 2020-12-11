var ingredientTemplate = document.getElementById('ingredientTemplate').innerHTML;
var imageTemplate = document.getElementById('imageTemplate').innerHTML;
var ingredientLines = [];
var images = [];
var measureUnits = [];

var baseUrl = 'https://localhost:44320/api';

function getExistingIngredientLines(id) {

    $.ajax({
        url: baseUrl + "/recipe/" + id, success: function (recipe) {
            Object.values(recipe.ingredientLines).forEach(ingredientLine => {
                addIngredientLine(ingredientLine.ingredient.name, ingredientLine.amount, ingredientLine.measureUnitInt, getNameForMeasureUnitIndex(ingredientLine.measureUnitInt));
            });

            Object.values(recipe.images).forEach(image => {
                addImage(image.fileName);
            });
        }
    });
}

function getMeasureUnits() {
    $.ajax({
        url: baseUrl + "/recipe/measure_units", success: function (units) {
            Object.values(units).forEach(unit => {
                measureUnits.push(unit);
            });
        }
    });
}

function getNameForMeasureUnitIndex(index) {
    var res;
    Object.values(measureUnits).forEach(unit => {
        if (index == unit.number) {
            res = unit.name;
        }
    });

    return res;
}

function saveUpdatedRecipe() {

    var recipe_id = parseInt($('#recipe_id').val());
    var recipe_title = $('#recipe_title').val();
    var recipe_instruction = $('#recipe_instruction').val();
    var row_version = $('#row_version').val();

    var RecipeDTO = {
        RowVer: row_version,
        Id: recipe_id,
        Title: recipe_title,
        Instruction: recipe_instruction,
        IngredientLines: [],
        Images: [],
        User: {
            Id: 1
        }
    };

    Object.values(ingredientLines).forEach(line => {
        RecipeDTO.IngredientLines.push({
            Ingredient: {
                Name: line.name
            },
            Amount: line.amount,
            MeasureUnit: parseInt(line.unit),
        });
    });

    Object.values(images).forEach(image => {
        RecipeDTO.Images.push({
            FileName: image.filename
        });
    });

    $.ajax({
        type: "PUT",
        url: baseUrl + "/recipe/update",
        contentType: "application/json",
        data: JSON.stringify(RecipeDTO)
    });
}

function addIngredientLine(ingredient_name,ingredient_amount,ingredient_measure_unit,ingredient_measure_unit_name) {
    
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

    $('#ingredient_add_name').val("");
    $('#ingredient_add_amount').val("");

    render();
}

function addImage(filename) {
    var identifier = Math.floor(Math.random() * Math.floor(Math.random() * Date.now()));

    images.push({ filename: filename, identifier: identifier });


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

        $('#recipe_images').append(renderedImages);
    }
}

function render() {
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
        
        $('#ingredient_lines').append(renderedIngredientLine);
    }
}

function removeIngredientLine(index, identifier) {
    ingredientLines.splice(index, 1);
    $("#ingredient_line_element_" + identifier).remove();
    render();
}

function removeImage(index, identifier) {
    images.splice(index, 1);
    $("#image_element_" + identifier).remove();
    renderImage();
}

$('#add_image_btn').click(function () {

    var filename = $('#image_add_name').val();

    addImage(filename);
});


$('#add_ingredient_btn').click(function () {
    var ingredient_name = $('#ingredient_add_name').val();
    var ingredient_amount = $('#ingredient_add_amount').val();
    var ingredient_measure_unit = $('#ingredient_add_measure_unit').children("option:selected").val();
    var ingredient_measure_unit_name = $('#ingredient_add_measure_unit').children("option:selected").text();

    addIngredientLine(ingredient_name, ingredient_amount, ingredient_measure_unit, ingredient_measure_unit_name);
});

$('#save_recipe_btn').click(function () {

    if (ingredientLines.length > 0) {
        saveUpdatedRecipe();
    } else {
        alert("Please add some ingredients");
        e.preventDefault();
    }

});

$(document).ready(function () {
    render();
    renderImage();
    getMeasureUnits();
});

