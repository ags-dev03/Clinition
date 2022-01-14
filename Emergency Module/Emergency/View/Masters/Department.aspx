<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/Dashboard/main.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="Emergency.View.Masters.Department" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headName" runat="server">
    <span>Laboratory Department Master</span>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <input type="hidden" runat="server" id="hdn_id" />
    <section class="table-background">
        <div class="table-responsive">
            <!--Table-->
            <div class="addNew">
                <a href="#" data-backdrop="static" data-target="#DepartmentPopup" data-toggle="modal" class="btn btn-custom mrg-l15 " onclick="Add();" title="Add New Department"><i class="fa fa-plus-circle"></i>Add New</a>
            </div>
            <table class="table table-striped" id="tbl_Department">
                <!--Table head-->
                <thead class="thead-background">
                    <tr>
                        <th class="table-bolder">#</th>
                        <th class="table-bolder">Name</th>
                        <th class="table-bolder">Sequence</th>
                        <th class="table-bolder">Status</th>
                        <th class="table-bolder">Action</th>
                    </tr>
                </thead>
            </table>
            <!--Table-->
        </div>
        <div class="showd  imgselget" id="imgselget">
            <div class="overlay"></div>
            <div class="img-showd">
                <span class="fadeout">X</span>
                <img src="" id="imgsel">
            </div>
        </div>
    </section>
    <section>
        <!-- The Modal -->
        <div class="modal" id="DepartmentPopup">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content pb-4">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <p class="modal-title text-center m-auto">Department Master</p>
                        <button type="button" class="close" data-dismiss="modal" onclick="Clear();">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <section>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" IsInPartialRendering="true">
                                <ContentTemplate>
                                    <div class="row  border-bottom-1">
                                        <div class="col-md-8 col-sm-8 col-8 mt-2">
                                            <h3 id="Department_head" class="heading-modal">Add Department</h3>
                                        </div>
                                        <div class="col-sm-2 pr-0 col-2 border-left-radius text-right">
                                            <asp:ImageButton ID="btn_save" src="/View/Admin/images/save.png" class="icon-t-modal" OnClick="btn_save_Click" OnClientClick="return Fn_Save();" title="Save" TabIndex="4" runat="server" />
                                        </div>
                                        <div class="col-sm-2 pl-0 col-2 border-right-radius text-center">
                                            <img src="/View/Admin/images/clear.png" class="icon-t-modal" onclick="Clear();" tabindex="5" title="Clear">
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col-md-4 col-sm-3">
                                            <label class="input-label-modal">Name<i class="fas fa-asterisk text-danger font_8"></i></label>
                                        </div>
                                        <div class="col-md-6 col-sm-9">
                                            <input id="txt_name" type="text" runat="server" tabindex="1" placeholder="..............................................." class="all-border BlankText border-bottom-2 font-12 width-100">
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-md-4 col-sm-3">
                                            <label class="input-label-modal">Sequence Number</label>
                                        </div>
                                        <div class="col-md-6 col-sm-9">
                                            <input id="txt_seq" type="text" runat="server" tabindex="2" placeholder="..............................................." class="all-border  border-bottom-2 font-12 width-100">
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-md-4 col-sm-3">
                                            <label class="input-label-modal">Status</label>
                                        </div>
                                        <div class="col-md-6 col-sm-9 status">
                                            <label class="switch">
                                                <input id="chk_status" runat="server" tabindex="3" type="checkbox" class="switchBox switch-small form-checkbox" data_on="success" data_off="danger" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder_BottomScripts">
    <script>
        var table;
        $(function () {
            DataBind();

        });
        function DataBind() {
            $.ajax({
                type: "POST",
                url: "/Classes/Services.asmx/GetAllDepartment",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {

            var data = eval("(" + response.d + ")");
            table = $('#tbl_Department').DataTable({
                bLengthChange: true,
                lengthMenu: [[5, 10, 20, 50, 100, 500, 1000], [5, 10, 20, 50, 100, 500, 1000]],
                iDisplayLength: 100,
                bFilter: true,
                bSort: true,
                destroy: true,
                bPaginate: true,
                "scrollCollapse": true,
                "scrollY": 350,
                "jQueryUI": true,
                data: data.Table1,
                columns: [{ data: 'row', sortable: false, searchable: false },
                { data: 'labdeptmas_name', sortable: true, searchable: true },
                { data: 'labdeptmas_seqno', sortable: true, searchable: true },
                {
                    data: 'labdeptmas_status', sortable: false, searchable: false,
                    render: function (data, type, row, meta) {
                        var actionButtons = '';
                        var chaeck = false;
                        if (data == "A")
                            chaeck = true;
                        actionButtons += $("<label/>", {
                            class: "switch",
                            html: $("<input/>", {
                                type: "checkbox",
                                Title: "Edit Status",
                                id: "Chk_status",
                                class: "chk_status switchBox switch-small form-checkbox",
                                value: row.labdeptmas_kid,
                                checked: chaeck,
                                "data-type": "IsActive",
                                'data-on': "success",
                                'data-off': "danger",
                            }).get(0).outerHTML +
                                $("<span/>", {
                                    class: "slider round",
                                }).get(0).outerHTML
                        }).get(0).outerHTML;

                        return actionButtons;
                    }
                },
                {
                    name: "Action", data: null, Title: "Action", sortable: false, searchable: false,
                    render: function (data, type, row, meta) {
                        var actionButtons = '';
                        actionButtons += $("<a/>", {
                            Title: "Edit Department",
                            class: "btn btn-default btn-sm tb-edit-btn",
                            href: "#",
                            onclick: "Edit(" + row.labdeptmas_kid + ")",
                            'data-toggle': "modal",
                            'data-target': "#DepartmentPopup",
                            'data-backdrop': "static",
                            html: $("<i/>", {
                                class: "fa fa-edit"
                            }).get(0).outerHTML
                        }).get(0).outerHTML;

                        actionButtons += $("<a/>", {
                            Title: "Delete Department",
                            class: "btn btn-default btn-sm tb-edit-btn",
                            href: "#",
                            onclick: "Delete(" + row.labdeptmas_kid + ")",
                            html: $("<i/>", {
                                class: "fa fa-trash"
                            }).get(0).outerHTML
                        }).get(0).outerHTML;

                        return actionButtons;
                    }
                }]
            });
        }
        function Edit(id) {
            $('[tabindex="1"]').focus();
            $('#Department_head').text('Edit Department ');
            $('#<%=hdn_id.ClientID%>').val(id);
            var EditDepartment = jqPost("/Classes/Services.asmx/GetDepartmentByID", "{'id':'" + id + "'}");

            if (EditDepartment != null && EditDepartment.Table1.length > 0) {

                $('#<%=txt_name.ClientID%>').val(EditDepartment.Table1[0].labdeptmas_name);
                $('#<%=txt_seq.ClientID%>').val(EditDepartment.Table1[0].labdeptmas_seqno);
                if (EditDepartment.Table1[0].labdeptmas_status == "A") {
                    $('#<%=chk_status.ClientID%>').prop("checked", true);
                }
                else
                    $('#<%=chk_status.ClientID%>').prop("checked", false);

            }
        }
        function Delete(id) {
            swal({
                title: "This record will be deleted",
                text: "Are you sure to proceed?",
                showCancelButton: true,
                type: "warning",
                confirmButtonColor: "#015184",
                confirmButtonText: "Yes",
                cancelButtonText: "No"
            },
                function (isConfirm) {
                    if (isConfirm) {
                        var DelDepartment = jqPost("/Classes/Services.asmx/DeleteUpdateDepartment", "{'id':'" + id + "','flag':'D','status':'" + false + "'}");
                        if (DelDepartment != null && DelDepartment.Table1.length > 0) {

                            if (DelDepartment.Table1[0].val == "1") {
                                MsgArray[0] = Msg["Delete"];
                                MsgColor(false);
                                MsgAlert();
                                table.destroy();
                                DataBind();
                            }
                            else if (DelDepartment.Table1[0].val == "-1") {
                                MsgArray[0] = Msg["LinkedRec"];
                                MsgColor(true);
                                MsgAlert();
                            }
                        }
                    }
                });
        }
        function Add() {
            Clear();
            $('#Department_head').text('Add Department');
            $('#<%=hdn_id.ClientID%>').val('');
        }

        function Save(SaveDepartment) {
            if (SaveDepartment == "1") {
                MsgArray[0] = Msg["Update"];
                MsgColor(false);
                MsgAlert();
                $('.close').click();
                //table.destroy();
                Clear();
                DataBind();
            }
            else if (SaveDepartment == "2") {
                MsgArray[0] = Msg["Insert"];
                MsgColor(false);
                MsgAlert();
                $('.close').click();
                //table.destroy();
                Clear();
                DataBind();
            }
            else if (SaveDepartment == "3") {
                MsgArray[0] = Msg["Already"];
                MsgColor(true);
                MsgAlert();
                $("#DepartmentPopup").modal('show');
            }
            else {
                MsgArray[0] = Msg["Error"];
                MsgColor(true);
                MsgAlert();
                $("#DepartmentPopup").modal('show');
            }

        }

        function Clear() {
            $('#<%=hdn_id.ClientID%>').val('');
            $('#Department_head').text('Add Department');
            $('#<%=txt_name.ClientID%>').val('');
            $('#<%=txt_seq.ClientID%>').val('');
            $('#<%=chk_status.ClientID%>').prop("checked", true);
            $('.border-bottom-Danger').each(function () {
                $(this).removeClass("border-bottom-Danger");
            });
        }

        $(document).on('change', '#Chk_status', function () {
            var status = $(this).is(":checked");

            var UpdateDepartment = jqPost("/Classes/Services.asmx/DeleteUpdateDepartment", "{'id':'" + $(this).val() + "','flag':'U','usrid':'" + $('#hdn_usrID').val() + "','status':'" + status + "'}");
            if (UpdateDepartment != null && UpdateDepartment.Table1.length > 0) {

                if (UpdateDepartment.Table1[0].val == "1") {
                    MsgArray[0] = Msg["Update"];
                    MsgColor(false);
                    MsgAlert();
                }
                else if (UpdateDepartment.Table1[0].val == "-1") {
                    MsgArray[0] = Msg["Error"];
                    MsgColor(false);
                    MsgAlert();
                }
            }
        });




    </script>
</asp:Content>
