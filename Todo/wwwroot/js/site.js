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

$('.ranked-item').ready(function () {
    //This doesn't work correctly, the idea was that this function should fire when ranked-item is loaded,
    //the email would then be grabbed from the hidden input field for the ranked item and sent to the server.
    //The profile div would also need to be scoped correctly so that not every field had the same profile regardless of the responsible party
    var email = 'test@test.com';
    $('.profile').load('/Profile/GetProfile?email=' + email);
});