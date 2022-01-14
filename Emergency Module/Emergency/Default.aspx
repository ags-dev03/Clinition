<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Emergency._Default" EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Emergency</title>
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta charset="utf-8">
    <%: Styles.Render("~/bundles/DefCss") %>
</head>
<body class="login-page">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hdn_otp" value="" />
        <input type="hidden" runat="server" id="hdn_userkid" value="" />
        <input type="hidden" runat="server" id="hdn_usercode" value="" />
        <input type="hidden" runat="server" id="hdn_userpasswd" value="" />
        <input type="hidden" runat="server" id="hdn_dt" value="" />
        <input type="hidden" runat="server" id="hdn_backDateFlag" value="" />
        <input type="hidden" runat="server" id="hdn_password" value="" />
        <input type="hidden" runat="server" id="hdn_usrcode" value="" />
        <input type="hidden" runat="server" id="hdn_ddt" value="" />
        <section>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="login-background">
                            <div class="row">
                                <div class="col-lg-12">
                                    <img src="Image/logo150.png" class="logo-img">
                                </div>
                                <div class="col-sm-6 text-center">
                                    <img src="Image/ambulance-login.png" class="img-responsive">
                                </div>
                                <div class="offset-lg-1 col-lg-4 col-sm-6">
                                    <h6 class="blue-text mb-2">
                                        <img src="Image/login-icon.png" class="login-icon"><b id="CompHead" runat="server">Login</b></h6>
                                    <p class="blue-text opacity-5 font-12">
                                        Login to <b>Emergency</b>
                                    </p>

                                    <div class="input-background gree-back">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-3 col-2">
                                                <i class="fas fa-user transform-center opacity-5"></i>
                                            </div>
                                            <div class="col-md-10 col-sm-9  col-10 pl-0">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <label class="font-12 opacity-5">CompName</label>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control font-12  p-0 login-input" placeholder="CompName" ID="txtCompName" name="compname"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="input-background gree-back">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-3 col-2">
                                                <i class="fas fa-user transform-center opacity-5"></i>
                                            </div>
                                            <div class="col-md-10 col-sm-9  col-10 pl-0">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <label class="font-12 opacity-5">Username</label>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control font-12  p-0 login-input" placeholder="Username" ID="txtUserName" name="username"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="input-background gree-back">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-3 col-2">
                                                <i class="fas fa-unlock-alt transform-center opacity-5"></i>
                                            </div>
                                            <div class="col-md-10 col-sm-9 col-10 pl-0">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <label class="font-12 opacity-5">Password</label>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" autocomplete="off" TextMode="Password" class="form-control font-12  p-0 login-input" MaxLength="50" placeholder="Password" ID="txtUserPass" name="username"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="offset-6 font-12 mt-2">
                                        </div>
                                        <div class="col-md-6  text-right">
                                            <a href="#" class="pr-2 font-12 opacity-5 bleck-text" data-toggle="modal" data-backdrop="static" data-target="#forget-password">Forget Password?</a>
                                        </div>
                                    </div>
                                    <div class="mt-4 buttons tra-30">
                                        <nav>
                                            <ul>
                                                <li><span>
                                                    <asp:Button runat="server" ID="btn_login" Text="Login Now" CssClass="btn" OnClick="btn_login_Click" OnClientClick="return LogIn(); " />
                                                    <asp:Button runat="server" ID="btn_loginnew" Text="Login Now" CssClass="d-none" OnClick="btn_loginNew_Click" /></span></li>
                                            </ul>
                                        </nav>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal" id="forget-password">
                        <div class="modal-dialog5  modal-dialog-centered">
                            <div class="modal-content">
                                <div class="col-lg-12">
                                    <div class="modal-header">
                                        <h4 class="modal-title text-center blue-text">Forget Password</h4>
                                        <button type="button" class="close" id="btn_close" data-dismiss="modal">&times;</button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="Forget_usrcode">
                                            <input type="text" id="txt_Usr" autocomplete="off" placeholder="Username" class="font-12 input-background gree-back" />
                                            <input type="password" id="txt_otp" maxlength="4" autocomplete="off" placeholder="Enter OTP" class="d-none int font-12 input-background gree-back" />
                                            <div class="hour d-none">
                                                <img src="/Image/hourglass.gif" />
                                                <span id="timer"></span>
                                            </div>
                                            <div class="modal-footer request">
                                                <div class="buttons">
                                                    <nav>
                                                        <ul>
                                                            <li class="mr-3 getotp "><span>
                                                                <input type="button" id="btn_otp" value="Get OTP" class="btn">
                                                            </span></li>
                                                            <li class="mr-3 submitotp d-none"><span>
                                                                <input type="button" id="btn_otpSubmit" value="Submit" class="btn">
                                                            </span></li>

                                                        </ul>
                                                    </nav>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="resetpass d-none">
                                            <input type="password" id="txt_newpass" maxlength="24" autocomplete="off" placeholder="Enter New Password"  class="font-12 input-background gree-back" />
                                            <input type="password" id="txt_renewpass" maxlength="24" autocomplete="off" placeholder="Re-type New Password" class="font-12 input-background gree-back" />
                                            <div class="modal-footer">
                                                <div class="buttons">
                                                    <nav>
                                                        <ul>
                                                            <li><span>

                                                                <input type="button" id="btn_reset" value="Reset Password" class="btn">
                                                            </span></li>
                                                        </ul>
                                                    </nav>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>


            </div>
        </section>
        <div class="MsgPopUp">
        </div>


        <%: Scripts.Render("~/bundles/modernizr") %>

        <script>
            $(function () {
                var time;
                //$('.int').numeric({});
                $('.Forget_mob').hide();
                $('.Forget_email').hide();
                $('.Forget_Pass').hide();
                $('.Forget_next').hide();
                var fullDate = new Date();
                var twoDigitMonth = fullDate.getMonth() + 1 + "";
                if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
                var twoDigitDate = fullDate.getDate() + "";
                if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
                var currentDate = twoDigitDate + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                console.log(currentDate)
                $("#<%=hdn_ddt.ClientID%>").val(currentDate);

            });

            function LogIn() {
                if ($('#txtUserName').val() != "") {
                    if ($('#txtUserPass').val() == "")
                        MsgArray[MsgArray.length] = Msg["UsrPass"];
                }
                else {
                    MsgArray[MsgArray.length] = Msg["UsrCode"];
                    if ($('#txtUserPass').val() == "")
                        MsgArray[MsgArray.length] = Msg["UsrPass"];
                }

                if (MsgArray.length > 0) {
                    MsgColor(true);
                    MsgAlert();
                    return false;
                }
                else
                    return true;
            }




            function Alert(msg, type) {
                MsgArray[MsgArray.length] = msg;
                MsgColor(type);
                MsgAlert();
            }

            function AlertRegExpiary(date) {
                MsgArray[MsgArray.length] = "Your Software License Renewal Charges is due for renewal by " + date + ". Kindly contact our customer relations Dept On 9782511333 for renewal.";
                MsgColor(true);
                MsgAlert();
            }

            function AlertRegExpiaryBefore(date) {
                MsgArray[MsgArray.length] = "Your Software License Renewal Charges is due for renewal by " + date + ". Kindly contact our customer relations Dept On 9782511333 for renewal.";
                MsgColor(true);
                MsgAlert();
            }


            function CorrectPass() {
                MsgArray[MsgArray.length] = Msg["PassMatch"];
                MsgColor(true);
                MsgAlert();
            }
            function CorrectUser() {
                MsgArray[MsgArray.length] = 'Please enter correct user code';
                MsgColor(true);
                MsgAlert();
            }


            function InValidUser() { }



            $(document).on('click', '#btn_close', function () {
                $('#<%=hdn_otp.ClientID%>').val('');
                $('#txt_Usr').removeAttr('disabled');
                $('#txt_otp').addClass('d-none');
                $('.getotp').removeClass('d-none');
                $('.submitotp').addClass('d-none');
                $('.Forget_usrcode').removeClass('d-none');
                $('.resetpass').addClass('d-none');
                $('.hour').addClass('d-none');
                $('#txt_Usr').val('');
                $('#txt_otp').val('');
                $('#txt_newpass').val('');
                $('#txt_renewpass').val('');
                document.getElementById('timer').innerHTML = "";
            });



            function timeOut() {
                $('#<%=hdn_otp.ClientID%>').val('');
                $('#txt_Usr').removeAttr('disabled');
                $('#txt_otp').addClass('d-none');
                $('#txt_otp').val('');
                $('.hour').addClass('d-none');
                $('.getotp').removeClass('d-none');
                $('.submitotp').addClass('d-none');
                document.getElementById('timer').innerHTML = "";
                MsgArray[MsgArray.length] = 'OTP expired try again.'
                MsgColor(true);
                MsgAlert();
            }


            function startTimer() {
                var presentTime = document.getElementById('timer').innerHTML;
                var timeArray = presentTime.split(/[:]+/);
                var m = timeArray[0];
                var s = checkSecond((timeArray[1] - 1));
                if (s == 59) { m = m - 1 }
                if (m >= 0) {
                    if (m == 0 && s < 30) {
                        $('#timer').attr('style', 'color:red');
                    }
                    document.getElementById('timer').innerHTML = m + ":" + s;
                    setTimeout(startTimer, 1000);
                }
            }

            function checkSecond(sec) {
                if (sec < 10 && sec >= 0) { sec = "0" + sec };
                if (sec < 0) { sec = "59" };
                return sec;
            }

            function AlertConfirm(message1, message2) {
                swal({
                    title: message1,
                    text: message2,
                    showCancelButton: true,
                    type: "warning",
                    confirmButtonColor: "#015184",
                    confirmButtonText: "Yes",
                    cancelButtonText: "No"
                },
                    function (isConfirm) {
                        $('#<%=btn_loginnew.ClientID%>').click();
                    });
            }



        </script>
    </form>
</body>
</html>

