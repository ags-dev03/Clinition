<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFileManager.aspx.cs" Inherits="Emergency.Scripts.tiny_mce.plugins.advimage.MyFileManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Manager</title>
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="expires" content="-1" />
</head>
<body>
    <form id="form1" runat="server">
    
<div id="center">
	<div id="topnav">
		<div id="topwrap">
			<div id="toolbar" class="toolbar caption">
				

				<div class="navigation">
					<img src="images_PM/filemanager.png" alt="" width="16" height="16">
					<strong>
					<span class="caption">File Manager</span></strong>
				</div>
				

				<ul id="tools" class="icons">
        					<li id="Li1" class="icon">
                                <asp:DropDownList CssClass="fileUpload" ID="ddlFolderName" runat="server">
                                <asp:ListItem Text="Select Folder Name" Value="all"></asp:ListItem>
                                </asp:DropDownList>
                
					<span class="caption" style="vertical-align:middle">Choose Folder</span>&nbsp;&nbsp;&nbsp;</li>            
					
					<li id="upload" class="icon">
                        <asp:FileUpload CssClass="fileUpload" ID="flupload" runat="server" />
                        <asp:RegularExpressionValidator ControlToValidate="flupload" ID="RegularExpressionValidator1" runat="server" 
                                                ErrorMessage="Only(jpeg files are allowed)" 
                                               ValidationGroup="tg" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G))$"></asp:RegularExpressionValidator>
                        <span style="vertical-align:middle">
                        <asp:ImageButton ImageUrl="images_PM/add.png" ValidationGroup="tg"  width="16" height="16" 
                            ID="imgbtnUpload" runat="server" onclick="imgbtnUpload_Click" />
					<span class="caption">Upload</span></span>&nbsp;&nbsp;&nbsp;</li>
					
					<li id="createdir" class="icon">
                        <asp:TextBox ID="txtCreateFolder" CssClass="textbox" runat="server"></asp:TextBox>
                        <span style="vertical-align:middle">
                        <asp:ImageButton ID="imgbtnCreateFolder" ImageUrl="images_PM/newfolder.png" 
                            Width="16px" Height="16px" runat="server" onclick="imgbtnCreateFolder_Click" /><span class="caption">Create folder</span>
                        </span>
                        </li>
                    <li id="refresh" class="icon">
                        <asp:ImageButton ID="imgbtnRefresh" Width="16px" Height="16px" 
                            ImageUrl="images_PM/reload.png" runat="server" onclick="imgbtnRefresh_Click" />
                        
                    <span style="vertical-align:middle" class="caption">Refresh</span>&nbsp;&nbsp;&nbsp;</li>                        
				</ul>
			</div>

			<br style=" clear: both;">
			<asp:Panel ID="errors" Visible="false" runat="server">
                <asp:Label ForeColor="Red" Font-Size="Large" ID="lblerrors" runat="server" Text="Label"></asp:Label>
			</asp:Panel>
			<br/>
		</div>
	</div>

	<div runat="server" id="listcontainer">
		<div id="folders">
			<div>
				<h2>Folders</h2>
                <div class="categories" id="category_list">
                    <asp:GridView GridLines="None" ID="GridView1" runat="server" Width="100%" 
                        AutoGenerateColumns="false" DataKeyNames="FolderID" onrowcommand="GridView1_RowCommand" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img id="img1" src="images_PM/newfolder.png" />
                                    <asp:LinkButton ID="Lbtn1" runat="server" CommandName='<%#Eval("FolderName") %>'
                                        CommandArgument='<%#Eval("FolderID") %>' Text='<%#Eval("FolderName") %>'></asp:LinkButton>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

			
			</div>
		</div>
	
		<div id="viewcontainer">
			

			<div style="display: none;" id="progress" class="pagenav">Loading...</div>

			<div class="viewmode">
				
				&nbsp;
			</div>

			<br/>

			<div id="filelist">
			<asp:Panel ID="delFolder" Visible="false" runat="server">
                <span class="caption">Delete Empty Folder</span>
                                        <asp:ImageButton ID="btnDelEmpty" runat="server" Text="Delete" 
                    ImageUrl="~/HMS/tinymce/jscripts/tiny_mce/plugins/advimage/images_PM/Icon_delete.gif" 
                    onclick="btnDelEmpty_Click" />
			</asp:Panel>
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="3"
RepeatDirection="Horizontal" DataKeyField="ImageID" onitemcommand="DataList1_ItemCommand">
                    <ItemTemplate>
                        <div id="file_0" class="avi file thumbnail" style="width: 120px;">
                            <div class="wrap">
                                <span id="Span1" runat="server" class="pic" style="height: 120px;">
                                    <div style="vertical-align:middle" align="center" >
                                        <div class="mid2">
                                        <table width="120px">
                                            <tr>
                                                <td style="width:100px; padding-top:10px; margin-left:3px">
                                                <asp:ImageButton ID="img1" runat="server" CommandName='<%# "images-editor/" + Session["folderName"] + "/" + Eval("image") %>' Height="100px" Width="100px" ImageUrl='<%# "~/HMS/tinymce/jscripts/tiny_mce/plugins/advimage/images-editor/" + Session["folderName"] + "/" + Eval("image") %>' />
                                                </td>
                                                
                                                
                                            </tr>                                            
                                        </table>
                                         


                                                
                                        </div>
                                    </div>
                                </span>
                            </div>
                            <div class="details">
                                <div class="wrap2">
                                    <div class="name" style="width: 74px;" title="sample.avi">
                                        </div>
                                    <div class="act">
                                    <span class="caption">Delete</span>
                                        <asp:ImageButton ID="b1" runat="server" Text="Delete" ImageUrl="~/HMS/tinymce/jscripts/tiny_mce/plugins/advimage/images_PM/Icon_delete.gif" CommandName="Delete" CommandArgument='<% #Eval("ImageID")%>' />
                                        </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
			
			
			
			
			</div>
		</div>
	</div>

	<script id="custom_dir_template" type="text/moxiecode-template">
		<![CDATA[
			<li><a href="#action" title="{path}" class="{type}">{title}</a></li>
		]]>
	</script>

	<script id="folders_template" type="text/moxiecode-template">
		<![CDATA[
			<li class="{type}"><a href="#action" title="{path}">{name}</a></li>
		]]>
	</script>

	<script id="text_template" type="text/moxiecode-template">
		<![CDATA[
			<div id="file_{index}" class="listview file {type}">
				<div class="wrap"></div>
				<div class="details">
					<div class="wrap2">
						<div class="name"><a href="#action" title="{name}" rel="file">{name}</a></div>
						<div class="act"><a href="#action" rel="menu"><img src="images_PM/act.gif" width="16" height="16" alt="" /></a></div>
					</div>
				</div>
			</div>
		]]>
	</script>

	<script id="thumb_template" type="text/moxiecode-template">
		<![CDATA[
			<div id="file_{index}" class="{type} file thumbnail" style="width:{thumb_width};">
				<div class="wrap">
				<a href="#action" title="{name}" rel="file" class="pic" style="height:{thumb_height};">
					<div class="mid"><div class="mid2"><img src="images_PM/loading_bg.gif" alt="{thumburl}" style="width:{width};height:{height}" class="thumbnailimage" /></div></div></a>
				</div>
				<div class="details">
					<div class="wrap2">
						<div class="name" style="width:{text_width};" title="{name}">{name}</div>
						<div class="act"><a href="#action" rel="menu"><img src="images_PM/act.gif" width="16" height="16" alt="" /></a></div>
					</div>
				</div>
			</div>
		]]>
	</script>
</div>


    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </form>
</body>
</html>
