// Write your Javascript code.
/* List */
$(".delete").click(function () {
    var bid = this.id;
    var objId = $(this).closest('tr').attr('id');
    $.ajax({
        type: "post",
        url: "/Entity/Delete?id="+objId,
        ajaxasync: true,
        success: function () {
            alert("success");
        },
        error: function (data) {
            alert(data.x);
        }
        });
});