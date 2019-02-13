
    function searchCust()
    {
        var cust = $('#client').val();
        $("td:contains('" + cust + "')").css('background', '#4dffa6');
    } 
    function clearSearch()
    {
        var cust = $('#client').val();
        if (cust != '')
        {
        $("td:contains('" + cust + "')").css("background", "none");
        } 
        $('#client').val('');
    } 