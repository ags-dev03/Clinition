<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/Dashboard/main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Emergency.View.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="headName" runat="server">
    <div class="col-md-11">
        <h4 class="darkblue"><i class="fas fa-ambulance text-danger mr-2"></i>EMERGENCY WARD</h4>
    </div>
    <div class="col-md-1 text-right icons">
        <i class="fas fa-th-large darkblue "></i>
        <i class="fas fa-list-ul ml-2 darkblue"></i>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row list grid">
        <div class="col-md-3 ">
            <div class="lightyellow">
                <div class="row yellow mx-0">

                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 1</h5>
                    </div>
                    <div class="col-md-4 text-right icons">
                        <div class="circle ">MLC</div>

                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <h6>Mr. Ramesh Sharma</h6>
                    </div>
                    <div class="col-md-12">
                        <div>Reg.No. - AGS00278954</div>
                    </div>
                    <div class="col-md-12">
                        <div>ER.No. - ER001545453</div>
                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-6">
                        <div>Age- 54/M</div>
                    </div>
                    <div class="col-md-6">
                        <div>Gender -Male</div>
                    </div>
                </div>
                <hr />
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <div><b>Vitas:</b> </div>
                    </div>
                    <div class="col-md-12">
                        <div>P-84/min BP. -130/80 SpO2-98%</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Povisional Diagnosis:</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>#left Tibia</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Comorbidities</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>T2DM, HTN, Hypothroidism, CDA</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Advise: </b>Admission</div>
                    </div>
                </div>
                <hr />
                <div class="text-center clearfix box-icon">
                    <ul class="text-danger">
                        <li class="float-left mb-2" data-toggle="modal" data-target="#addressModal"><i class="fas fa-address-card "></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="addressModal" tabindex="-1" aria-labelledby="addressModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="addressModalLabel">View Details</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="view-details mb-2 clearfix">
                                            <ul>
                                                <li>
                                                    <a href="#" class="">Pathology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Radiology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Patient ledger</a></li>
                                                <li>
                                                    <a href="#" class="">View prescription </a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View discharge</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View OT notes</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <li class="float-left mb-2" data-toggle="modal" data-target="#syncModal"><i class="fas fa-sync-alt text-danger"></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="syncModal" tabindex="-1" aria-labelledby="syncModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="syncModalLabel">Switch case</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                            <label class="btn btn-secondary backgroundblue active">
                                                <input type="radio" name="options" id="option1" checked>
                                                Normal
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option2">
                                                Moderately sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option3">
                                                Severly sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option4">
                                                Dead
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <li class="float-left mb-2 " data-toggle="tooltip" data-placement="top" title="Inform discharge">
                            <i class="fas fa-info-circle text-danger"></i>
                        </li>
                        <li class="float-left mb-2">
                            <i class="far fa-smile-beam text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="View Bio"><i class="fas fa-bong"></i></li>
                        <li class="float-left mb-2">
                            <i class="fas fa-rupee-sign text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="discharge Patient">
                            <i class="fas fa-door-open"></i>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="lightgreen">
                <div class="row green mx-0">

                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 2</h5>
                    </div>
                    <div class="col-md-4 text-right  icons">


                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <h6>Mr. Ramesh Sharma</h6>
                    </div>
                    <div class="col-md-12">
                        <div>Reg.No. - AGS00278954</div>
                    </div>
                    <div class="col-md-12">
                        <div>ER.No. - ER001545453</div>
                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-6">
                        <div>Age- 54/M</div>
                    </div>
                    <div class="col-md-6">
                        <div>Gender -Male</div>
                    </div>
                </div>
                <hr />
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <div><b>Vitas:</b> </div>
                    </div>
                    <div class="col-md-12">
                        <div>P-84/min BP. -130/80 SpO2-98%</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Povisional Diagnosis:</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>#left Tibia</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Comorbidities</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>T2DM, HTN, Hypothroidism, CDA</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Advise: </b>Admission</div>
                    </div>
                </div>
                <hr />
                <div class="text-center clearfix box-icon">
                    <ul class="text-danger">
                        <li class="float-left mb-2" data-toggle="modal" data-target="#addressModal"><i class="fas fa-address-card "></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="addressModal" tabindex="-1" aria-labelledby="addressModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="addressModalLabel">View Details</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="view-details mb-2 clearfix">
                                            <ul>
                                                <li>
                                                    <a href="#" class="">Pathology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Radiology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Patient ledger</a></li>
                                                <li>
                                                    <a href="#" class="">View prescription </a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View discharge</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View OT notes</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <li class="float-left mb-2" data-toggle="modal" data-target="#syncModal"><i class="fas fa-sync-alt text-danger"></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="syncModal" tabindex="-1" aria-labelledby="syncModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="syncModalLabel">Switch case</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                            <label class="btn btn-secondary backgroundblue active">
                                                <input type="radio" name="options" id="option1" checked>
                                                Normal
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option2">
                                                Moderately sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option3">
                                                Severly sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option4">
                                                Dead
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <li class="float-left mb-2 " data-toggle="tooltip" data-placement="top" title="Inform discharge">
                            <i class="fas fa-info-circle text-danger"></i>
                        </li>
                        <li class="float-left mb-2">
                            <i class="far fa-smile-beam text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="View Bio"><i class="fas fa-bong"></i></li>
                        <li class="float-left mb-2">
                            <i class="fas fa-rupee-sign text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="discharge Patient">
                            <i class="fas fa-door-open"></i>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="red">
                <div class="row darkred mx-0">

                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 3</h5>
                    </div>
                    <div class="col-md-4 text-right  icons">


                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <h6>Mr. Ramesh Sharma</h6>
                    </div>
                    <div class="col-md-12">
                        <div>Reg.No. - AGS00278954</div>
                    </div>
                    <div class="col-md-12">
                        <div>ER.No. - ER001545453</div>
                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-6">
                        <div>Age- 54/M</div>
                    </div>
                    <div class="col-md-6">
                        <div>Gender -Male</div>
                    </div>
                </div>
                <hr />
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <div><b>Vitas:</b> </div>
                    </div>
                    <div class="col-md-12">
                        <div>P-84/min BP. -130/80 SpO2-98%</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Povisional Diagnosis:</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>#left Tibia</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Comorbidities</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>T2DM, HTN, Hypothroidism, CDA</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Advise: </b>Admission</div>
                    </div>
                </div>
                <hr />
                <div class="text-center clearfix box-icon">
                    <ul class="text-danger">
                        <li class="float-left mb-2" data-toggle="modal" data-target="#addressModal"><i class="fas fa-address-card "></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="addressModal" tabindex="-1" aria-labelledby="addressModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="addressModalLabel">View Details</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="view-details mb-2 clearfix">
                                            <ul>
                                                <li>
                                                    <a href="#" class="">Pathology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Radiology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Patient ledger</a></li>
                                                <li>
                                                    <a href="#" class="">View prescription </a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View discharge</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View OT notes</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <li class="float-left mb-2" data-toggle="modal" data-target="#syncModal"><i class="fas fa-sync-alt text-danger"></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="syncModal" tabindex="-1" aria-labelledby="syncModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="syncModalLabel">Switch case</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                            <label class="btn btn-secondary backgroundblue active">
                                                <input type="radio" name="options" id="option1" checked>
                                                Normal
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option2">
                                                Moderately sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option3">
                                                Severly sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option4">
                                                Dead
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <li class="float-left mb-2 " data-toggle="tooltip" data-placement="top" title="Inform discharge">
                            <i class="fas fa-info-circle text-danger"></i>
                        </li>
                        <li class="float-left mb-2">
                            <i class="far fa-smile-beam text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="View Bio"><i class="fas fa-bong"></i></li>
                        <li class="float-left mb-2">
                            <i class="fas fa-rupee-sign text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="discharge Patient">
                            <i class="fas fa-door-open"></i>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class=" lightgray">
                <div class="row gray mx-0">

                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 4</h5>
                    </div>
                    <div class="col-md-4 text-right  icons">


                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <h6>Mr. Ramesh Sharma</h6>
                    </div>
                    <div class="col-md-12">
                        <div>Reg.No. - AGS00278954</div>
                    </div>
                    <div class="col-md-12">
                        <div>ER.No. - ER001545453</div>
                    </div>
                </div>
                <div class="row  mx-0">
                    <div class="col-md-6">
                        <div>Age- 54/M</div>
                    </div>
                    <div class="col-md-6">
                        <div>Gender -Male</div>
                    </div>
                </div>
                <hr />
                <div class="row  mx-0">
                    <div class="col-md-12">
                        <div><b>Vitas:</b> </div>
                    </div>
                    <div class="col-md-12">
                        <div>P-84/min BP. -130/80 SpO2-98%</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Povisional Diagnosis:</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>#left Tibia</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Comorbidities</b></div>
                    </div>
                    <div class="col-md-12">
                        <div>T2DM, HTN, Hypothroidism, CDA</div>
                    </div>
                    <div class="col-md-12">
                        <div><b>Advise: </b>Admission</div>
                    </div>
                </div>
                <hr />
                <div class="text-center clearfix box-icon">
                    <ul class="text-danger">
                        <li class="float-left mb-2" data-toggle="modal" data-target="#addressModal"><i class="fas fa-address-card "></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="addressModal" tabindex="-1" aria-labelledby="addressModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="addressModalLabel">View Details</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="view-details mb-2 clearfix">
                                            <ul>
                                                <li>
                                                    <a href="#" class="">Pathology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Radiology reports</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">Patient ledger</a></li>
                                                <li>
                                                    <a href="#" class="">View prescription </a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View discharge</a></li>
                                                <li>
                                                    <a href="#" class="ml-2">View OT notes</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <li class="float-left mb-2" data-toggle="modal" data-target="#syncModal"><i class="fas fa-sync-alt text-danger"></i></li>
                        <!-- Modal -->
                        <div class="modal fade" id="syncModal" tabindex="-1" aria-labelledby="syncModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="syncModalLabel">Switch case</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                            <label class="btn btn-secondary backgroundblue active">
                                                <input type="radio" name="options" id="option1" checked>
                                                Normal
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option2">
                                                Moderately sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option3">
                                                Severly sick
                                            </label>
                                            <label class="btn btn-secondary backgroundblue">
                                                <input type="radio" name="options" id="option4">
                                                Dead
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <li class="float-left mb-2 " data-toggle="tooltip" data-placement="top" title="Inform discharge">
                            <i class="fas fa-info-circle text-danger"></i>
                        </li>
                        <li class="float-left mb-2">
                            <i class="far fa-smile-beam text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="View Bio"><i class="fas fa-bong"></i></li>
                        <li class="float-left mb-2">
                            <i class="fas fa-rupee-sign text-danger"></i>
                        </li>
                        <li class="float-left mb-2" data-toggle="tooltip" data-placement="top" title="discharge Patient">
                            <i class="fas fa-door-open"></i>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-3 mt-3">
            <div class=" lightblue bg-img">
                <div class="row backgroundblue mx-0">
                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 1</h5>
                    </div>
                    <div class="col-md-4 text-right icons">
                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3 mt-3">
            <div class=" lightblue bg-img">
                <div class="row backgroundblue mx-0">
                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 2</h5>
                    </div>
                    <div class="col-md-4 text-right icons">
                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3 mt-3">
            <div class=" lightblue bg-img">
                <div class="row backgroundblue mx-0">
                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 3</h5>
                    </div>
                    <div class="col-md-4 text-right icons">
                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3 mt-3">
            <div class=" lightblue bg-img">
                <div class="row backgroundblue mx-0">
                    <div class="col-md-8 mt-2 text-white">
                        <h5>Bed 4</h5>
                    </div>
                    <div class="col-md-4 text-right icons">
                        <div class="dropdown" style="float: right;">
                            <i class="fas fa-list-ul ml-2 text-white dropbtn "></i>
                            <div class="dropdown-content text-left">
                                <a href="#" class="d-flex"><i class="fas fa-user"></i>&nbsp; &nbsp; &nbsp; <span>Order Consultation</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-flask"></i>&nbsp; &nbsp; &nbsp;<span>Order Investigation-pathoglogy-Radiology</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-mortar-pestle"></i>&nbsp; &nbsp; &nbsp;<span> Order Pharmacy</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-diagnoses"></i>&nbsp; &nbsp; &nbsp;<span>Order Diet</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>Transfer to IPD</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-syringe"></i>&nbsp; &nbsp; &nbsp; <span>Surgery Advise</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-notes-medical"></i>&nbsp; &nbsp; &nbsp; <span>Book OT</span></a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>OT Notes</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-sync"></i>&nbsp; &nbsp; &nbsp; <span>MLC Details</span> </a>
                                <a href="#" class="d-flex"><i class="far fa-address-book"></i>&nbsp; &nbsp; &nbsp; <span>Prepair Summary</span> </a>
                                <a href="#" class="d-flex"><i class="fas fa-ambulance"></i>&nbsp; &nbsp; &nbsp; <span>Book Ambulance</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BottomScripts" runat="server">
</asp:Content>
