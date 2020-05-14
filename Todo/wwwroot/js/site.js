/* TodoList/Detail.cshtml */
$('#hide-items-toggle').click(function () {
    if ($(this).prop("checked") === true) {
        $(".is-done").addClass("todo-item-complete");
    }
    else{
        $(".is-done").removeClass("todo-item-complete");
    }
});


$('#sort-button').click(function () {
    var listGroup = $('.list-group');
    var rankedItems = listGroup.children('.ranked-item');

    rankedItems.detach().sort(function (a, b) {
        return $(a).data('sort') - $(b).data('sort');
    });
    listGroup.append(rankedItems);
});