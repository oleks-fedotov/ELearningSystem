function disableFunction(button) {
    button.css("disabled", "true");
}

function MoveUp(container, elem) {
    var thisrow = $(elem).closest("tr");
    if (thisrow[0] != container.children().first()[0]) {
        if ($("#SaveChangesBut").css("display") == "none") { $("#SaveChangesBut").css("display", "inline"); }
        var prevrow = thisrow.prev();
        var thisOrderNumber = thisrow.children().first().children().last();
        thisOrderNumber.val(Number(Number(thisOrderNumber.val()) - 1));
        var prevOrderNumber = prevrow.children().first().children().last();
        prevOrderNumber.val((Number(Number(prevOrderNumber.val()) + 1)));
        thisrow.insertBefore(prevrow);
    }
};

function MoveDown(container, elem) {
    var thisrow = $(elem).closest("tr");
    if (thisrow[0] != container.children().last()[0]) {
        if ($("#SaveChangesBut").css("display") == "none") { $("#SaveChangesBut").css("display", "inline"); }
        var nextrow = thisrow.next();
        var thisOrderNumber = thisrow.children().first().children().last();
        thisOrderNumber.val(Number(Number(thisOrderNumber.val()) + 1));
        var nextOrderNumber = nextrow.children().first().children().last();
        nextOrderNumber.val(Number(Number(nextOrderNumber.val()) - 1));
        thisrow.insertAfter(thisrow.next());
    }
};

function AddTopic(container) {
    if ($("#SaveChangesBut").css("display") == "none") { $("#SaveChangesBut").css("display", "inline"); }
    var topicName = $("#newTopicName").val();
    $("#newTopicName").prop("value", "");
    var courseId = $("#CourseId").val();
    if (isNaN(Number(container.children().last().children().first().children().last().val())))
        var topicNumber = 1;
    else
        var topicNumber = Number(container.children().last().children().first().children().last().val()) + 1;
    var index = topicNumber - 1;
    container.append(
    '<tr><td><div class="col-sm-12"><div class="form-inline"><div class="form-group">' +
        '<button onclick="MoveUp($(this).closest(\'tbody\'), this); return false;" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-arrow-up"></span></button> ' +
        '<button onclick="MoveDown($(this).closest(\'tbody\'), this); return false;" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-arrow-down"></span></button> ' +
        '<input id="topicId" name="topicId" type="hidden" value="00000000-0000-0000-0000-000000000000"></form>' +
        '</div></div></div><input name="Topics[' + index + '].ID" type="hidden" value="00000000-0000-0000-0000-000000000000">' +
        '<input name="Topics[' + index + '].CourseId" type="hidden" value="' + courseId + '">' +
            '<input data-val="true" data-val-number="The field OrderNumber must be a number." data-val-required="The OrderNumber field is required." name="Topics[' + index +
            '].OrderNumber" type="hidden" value="' + topicNumber + '">' +
        '</td><td><input name="Topics[' + index + '].TopicName" type="hidden" value="' + topicName + '">' + topicName + '</td><td>0</td><td>0</td></tr>')
};

function DeleteCourseConfirmation(courseName) {
    var flag = window.confirm("Are you sure, you want to delete course - " + courseName + "?");
    if (flag) return true;
    else return false;
};


