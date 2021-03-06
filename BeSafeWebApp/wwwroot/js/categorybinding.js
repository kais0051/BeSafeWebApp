﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("#loaderbody").addClass('hide');
    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    // $('#view-all').html(res.html)
                    $('#accordion').html(res.html)

                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

DeleteCategory = (url, id) => {
    if (confirm('Are you sure to delete this record ?')) {
        try {

            $.ajax({
                type: 'POST',
                url: url,
                data: { CategoryId: id },
                contentType: 'application/x-www-form-urlencoded',
                // contentType: "application/json",
                // dataType: 'json',
                //contentType: false,
                //processData: false,
                success: function (res) {
                    //$('#view-all').html(res.html);
                    $('#accordion').html(res.html)
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    //$('#view-all').html(res.html);
                    $('#accordion').html(res.html)
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

showInPopupMasterItemsSet = (url, title, catid) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })

    jQueryAjaxPostForCategoryItem = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        // $('#view-all').html(res.html)
                        var catdiv = '#divCategoryMasterItemPanel'+$('#CategoryId').val();
                        $(catdiv).html(res.html);

                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        $('#form-modal').modal('hide');
                    }
                    else
                        $('#form-modal .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }


    //DeleteCategoryItem = (url,id,catid) => {
    //    if (confirm('Are you sure to delete this record ?')) {
    //        try {
    //            $.ajax({
    //                type: 'POST',
    //                url: url,
    //                data: { ItemId: id, CategoryId: catid},
    //                contentType: 'application/x-www-form-urlencoded',
    //                // contentType: "application/json",
    //                // dataType: 'json',
    //                //contentType: false,
    //                //processData: false,
    //                success: function (res) {
    //                    //$('#view-all').html(res.html);
    //                    var catdiv = '#divCategoryMasterItemPanel' + catid;
    //                    $(catdiv).html(res.html);
    //                   // $('#accordion').html(res.html)
    //                },
    //                error: function (err) {
    //                    console.log(err)
    //                }
    //            })
    //        } catch (ex) {
    //            console.log(ex)
    //        }
    //    }

    //    //prevent default form submit event
    //    return false;
    //}

}