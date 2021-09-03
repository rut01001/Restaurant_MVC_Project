//$(document).ready(function () {

//});

function ShowDishes(numCategory, IsAdmin, IsShowDishesAllergy = true) {
    categoryId = numCategory;
    $.ajax({
        type: "POST",
        url: '/Home/GetDetailsDishesForCategory',
        data: { keyCategory: numCategory, IsAllergy: IsShowDishesAllergy },
        //dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null && data.result != null) {

                LoadData(data.result, IsAdmin);
            }
        },
        error: function (error) {
            alert("Error" + error);
        }
    });

}
function LoadData(data, IsAdmin) {
    var categoryId;
    var tbody = $("<tbody />"), tr;
    $.each(JSON.parse(data), function (_, obj) {
        tr = $("<tr />");
        $.each(obj, function (columnName, columnVal) {
            if (columnName == "DISH_ID")
                tr.append('<td class="colDishId" style="display: none;">' + columnVal + '</td>');
            else if (columnName == "CATEGORY_ID")
                tr.append('<td class="colCategoryId" style="display: none;">' + columnVal + '</td>');
            else {
                if (columnName == "CONTAINS_ALLERGENG" && IsAdmin) {
                    if (columnVal == true)
                        tr.append('<td><input class="cbAllergy" type="checkbox" checked="checked" disabled="disabled" /></td>');

                    else {
                        tr.append('<td><input type="checkbox" disabled="disabled"/></td>');
                    }
                }
                if (columnName == "CONTAINS_ALLERGENG" && !IsAdmin) {
                    if (columnVal == true)
                        tr.append('<td><input class="cbAllergy" type="checkbox" checked="checked" disabled="disabled" style="display:none;"/></td>');

                    else {
                        tr.append('<td><input type="checkbox" disabled="disabled" style="display:none;"/></td>');
                    }
                }
                else if (columnName != "CONTAINS_ALLERGENG")
                    tr.append("<td>" + columnVal + "</td>")
            }
        });
        if (IsAdmin) {
            var dishId = tr[0].querySelector('.colDishId').outerText;
            var categoryId = tr[0].querySelector('.colCategoryId').outerText;
            tr.append('<td><input type="button" class="btnDeleteDish" value="מחיקה" onclick="deleteDish(' + dishId + ');ShowDishes(' + categoryId + "," + IsAdmin + ');"/></td>')
            tr.append('<td><input type="button" class="btnDeleteDish" value="עריכה" onclick="editDish(' + dishId + ');ShowDishes(' + categoryId + "," + IsAdmin + ');"/></td>')
        }
        tr.appendTo(tbody);
    });
    if ($('#table1 tbody')[0] != undefined)
        $('#table1 tbody')[0].outerHTML = "";
    tbody.appendTo("#table1");
    $('.cbAllergy').parent().parent().css("background-color", "red");
    
}
function DeleteLastTable() {
    if ($('#table1 tbody') != undefined && $('#table1 tbody').length > 0) {
        $('#table1 tbody')[0].outerHTML = "";
    }
}
function clickOnCategory(numCategory, IsAdmin) {
    DeleteLastTable();
    if ($('#cbIsAllergy').is(':checked')) {
        /*הצגת מנות ללא אלרגנים*/
        ShowDishes(numCategory, IsAdmin, false);
    }
    else {
        /*כל  המנות*/
        ShowDishes(numCategory, IsAdmin, true);
    }
    if (IsAdmin)
        $('#btnAddDishModal').css('visibility', 'visible');
}
function deleteDish(dishId) {

    $.ajax({
        type: "POST",
        url: '/Home/deleteDish',
        data: { idDish: dishId },
        //dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function () {

        },
        error: function (error) {
            alert("Error" + error);
        }
    });


}
function isAdmin(adminOrCustomer) {
    $.ajax({
        type: "POST",
        url: '/Home/saveStatusOfUser',
        data: { statusOfUser: adminOrCustomer },
        //dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function () {

        },
        error: function (error) {
            alert("Error" + error);
        }
    });

}
function editDish(dishId) {
    
    $.ajax({
        type: "POST",
        url: '/Home/SaveDishId',
        data: { DishId: dishId },
        //dataType: "json",
        //contentType: "application/json; charset=utf-8",
        success: function () {

        },
        error: function (error) {
            alert("Error" + error);
        }
    });
    $('#ModalEdit').modal('show');

}