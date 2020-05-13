/* TodoList/Detail.cshtml */
$('#hide-items-toggle').click(function () {
    if ($(this).prop("checked") === true) {
        $(".is-done").addClass("todo-item-complete");
    }
    else{
        $(".is-done").removeClass("todo-item-complete");
    }
});