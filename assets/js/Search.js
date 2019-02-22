
    function searchCust()
    {
        var cust = $('#client').val();
        $('.table td').parent().hide();
        $("td:contains('"+ cust +"')").parent().show();
    } 
    function clearSearch()
    {
        var cust = $('#client').val();
        if (cust != '')
        {
        $('.table td').parent().show();
        } 
        $('#client').val('');
    } 