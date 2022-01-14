<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PdfPresSave.aspx.cs"
    ValidateRequest="false" Inherits="Emergency.PDFHelper.PdfPresSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/Jquery.js" type="text/javascript"></script>
    <script src="../Scripts/GS_PDFParser.js" type="text/javascript"></script>
    <script src="../Scripts/NotoWord.js" type="text/javascript"></script>
    <style type="text/css">
        p, table {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body style="visibility: hidden;">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnfld_Page" runat="server" />
        <asp:HiddenField ID="hdnfld_HeaderHeight" Value="10" runat="server" />
        <asp:HiddenField ID="hdnfld_FooterHeight" Value="10" runat="server" />
        <asp:HiddenField ID="hdnfld_OPHHeight" Value="10" runat="server" />
        <asp:HiddenField ID="hdnfld_LPFHeight" Value="10" runat="server" />
        <asp:HiddenField ID="hdnfld_Header" runat="server" />
        <asp:HiddenField ID="hdnfld_Footer" runat="server" />
        <asp:HiddenField ID="hdnfld_PageType" runat="server" />
        <asp:HiddenField ID="hdnfld_PageOrientation" runat="server" />
        <asp:HiddenField ID="hdnfld_OPH" runat="server" />
        <asp:HiddenField ID="hdnfld_LPF" runat="server" />
        <asp:HiddenField ID="hdn_leftmargin" runat="server" />
        <asp:HiddenField ID="hdn_rightmargin" runat="server" />
        <asp:HiddenField ID="hdn_HeaderBodyMargin" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_BodyFooterMargin" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_SpName" runat="server" />
        <asp:HiddenField ID="hdn_activityID" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_SaveID" runat="server" />
        <asp:HiddenField ID="hdn_count" runat="server" value ="1"/>
        <div id="Container">
            <div id="header">
                <asp:Literal ID="ltrl_Header" runat="server"></asp:Literal>
            </div>
            <div id="page">
                <asp:Repeater ID="rptr_Format" runat="server">
                    <ItemTemplate>
                        <%#Eval("FormatDetail_Format") %>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="footer">
                <asp:Literal ID="ltrl_Footer" runat="server"></asp:Literal>
            </div>
            <div id="OPH">
                <asp:Literal ID="ltrl_OPH" runat="server"></asp:Literal>
            </div>
            <div id="LPF">
                <asp:Literal ID="ltrl_LPF" runat="server"></asp:Literal>
            </div>
            <asp:Button ID="btn_createPDf" Text="Create Me" runat="server" OnClick="btn_createPDf_Click" />
        </div>
    </form>
    <script type="text/javascript">
        //this code handles the F5/Ctrl+F5/Ctrl+R
        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event)
                keycode = window.event.keyCode;
            else if (e)
                keycode = e.which;

            // Mozilla firefox
            if ($.browser.mozilla) {
                if (keycode == 116 || (e.ctrlKey && keycode == 82)) {
                    if (e.preventDefault) {
                        e.preventDefault();
                        e.stopPropagation();
                        window.location = $(location).attr("href").replace('#', '');
                    }
                }
            }
            // IE
            else if ($.browser.msie) {
                if (keycode == 116 || (window.event.ctrlKey && keycode == 82)) {
                    window.event.returnValue = false;
                    window.event.keyCode = 0;
                    window.location = $(location).attr("href").replace('#', '');
                }
            }
            else {
                if (keycode == 116 || (e.ctrlKey && keycode == 82)) {
                    if (e.preventDefault) {
                        e.preventDefault();
                        e.stopPropagation();
                        window.location = $(location).attr("href").replace('#', '');
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(function () {
            //for Database to get Stored Procedure code  
            if (getParameterByName('PageID') != "") {
                $.ajax({
                    type: "POST",
                    url: "/Classes/Services.asmx/GS_GetSpName",
                    data: "{'PageID': '" + getParameterByName('PageID') + "'}",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",
                    success: function (retVal) {
                        var obj = eval("(" + retVal.d + ")");
                        $('#hdn_SpName').val(obj.Table1[0].Format_SPName);
                    },
                    error: function () { }
                });
            }
            
            if (getParameterByName('PageID') == "2") {
                console.log(getParameterByName('ptmastId'), getParameterByName('ptOPDID'), $('#hdn_SpName').val());
                getData("/Classes/Services.asmx/PriscriptionSave", "{ptmastid:'" + getParameterByName('ptmastId') + "',ptopdid:'" + getParameterByName('ptOPDID') + "',spname:'" + $('#hdn_SpName').val() + "'}");
            }
        });
        function close() {
            window.close();
        }
    </script>
</body>
</html>
