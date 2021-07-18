$("#logInPage").ready(function (){
    $("#logIn").click(function (){
        const username= $("#usertName").val();
        const password = $("#passcode").val();
        $.ajax({
            url:"api/authorization?userName="+username+"&password="+password,
            type:"GET",
            dataType: "json",
            success: function () {
                $.ajax({
                    url:"/token",
                    type: "POST",
                    contentType: "/x-www-form-urlencoded",
                    data:{grant_type: "password"},
                    success:function (data){
                        sessionStorage.setItem("token",data["access_token"]);
                        window.location.replace("/employee.html");
                    }
                });
            }
        });
    });
});