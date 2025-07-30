using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Models;
using DataViewConfig.Pages.Popups;
using DbModels;
using Newtonsoft.Json;
using SqlSugar;
using static DataViewConfig.EnumerationExtension;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class NewRpcEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        public int RequestID { get; set; }
        public string RequestInternalName { get; set; }
        public int RequestSystemId { get; set; }
        public ObservableCollection<DbModels.dv_system> RequestSystemLst { get; set; }

        public ObservableCollection<Controls.RpcParamMultipleCheckboxModel> ParamMultiCheckLst { get; set; }

        public RequestPreConditionType RequestPrecondition { get; set; }
        public string ParamIdLst { get; set; }
        public ECSCommType EcsComm { get; set; }
        public ParamChangeType ParamChange { get; set; }
        public string ParamSeperator { get; set; }
        /// <summary>
        /// mq通讯：msg type; rest通讯：api路由；opc通讯：选择cmdIndex方式时，对应index索引
        /// </summary>
        public string MsgType { get; set; }
        
        public string RequestDesc { get; set; }
        public bool IsWriteSingleTag { get; set; }
        public bool IsWriteArgsEventsTag { get; set; }
        public bool IsWriteCmdIdIndexValueTag { get; set; }
        /// <summary>
        /// 1=写单个点，并等待Feedback；=0表示写Args/Events并等待Return
        /// </summary>
        //public int DestSingleTagType { get; set; }
        public string RequestTagInternalName { get; set; }
        public DbModels.dv_tag RequestTag { get; set; }
        public string RequestFeedbackTagInternalName { get; set; }
        public DbModels.dv_tag RequestFeedbackTag { get; set; }
        public string ArgsTagName { get; set; }
        public DbModels.dv_tag ArgsTag { get; set; }
        public string EventTagName { get; set; }
        public DbModels.dv_tag EventTag { get; set; }
        public string ReturnTagName { get; set; }
        public DbModels.dv_tag ReturnTag { get; set; }
        public string CmdIdTagName_Dv2Plc { get; set; }
        public DbModels.dv_tag CmdIdTag_Dv2Plc { get; set; }
        public string CmdIndexTagName_Dv2Plc { get; set; }
        public DbModels.dv_tag CmdIndexTag_Dv2Plc { get; set; }
        public string CmdValueTagName_Dv2Plc { get; set; }
        public DbModels.dv_tag CmdValueTag_Dv2Plc { get; set; }
        public string CmdIdTagName_Plc2Dv { get; set; }
        public DbModels.dv_tag CmdIdTag_Plc2Dv { get; set; }
        public string CmdIndexTagName_Plc2Dv { get; set; }
        public DbModels.dv_tag CmdIndexTag_Plc2Dv { get; set; }
        public string CmdValueTagName_Plc2Dv { get; set; }
        public DbModels.dv_tag CmdValueTag_Plc2Dv { get; set; }
        public string CmdReturnTagName_Plc2Dv { get; set; }
        public DbModels.dv_tag CmdReturnTag_Plc2Dv { get; set; }
        public bool IsOpenParamCombox { get; set; }
        public string ArrayItemsParamStr { get; set; }
        public string ParamExpressionStr { get; set; }
        private ParamModel selectedParam;
        public ParamModel SelectedParam 
        {
            get
            {
                return selectedParam;
            }
            set
            {
                selectedParam = value;
                if (value == null)
                {
                    return;
                }
                
                var paramModel = value as ParamModel;
                this.ParameterInternalName = paramModel.ParamName;
                var expressonStr = string.IsNullOrEmpty(paramModel.ExpressionStr) ? "" : $"\r\n转换表达式:{paramModel.ExpressionStr}";
                switch (paramModel.ParamSource)
                {
                    case RequestParamSource.CONSTANT:
                        this.ParamTotalInfo = $"参数值:{paramModel.ConstValue}{expressonStr}\r\n参数类型:{paramModel.TargetValType}";
                        break;
                    case RequestParamSource.CONTROL:
                        this.ParamTotalInfo = $"控件:{paramModel.ControlInternalName}{expressonStr}\r\n参数类型:{paramModel.TargetValType}";
                        break;
                    case RequestParamSource.JSON_TAG:
                        this.ParamTotalInfo = $"点名(内部):{paramModel.TagInternalName},字段路径:{paramModel.TagValueJsonPath}\r\n{expressonStr}\r\n参数类型:{paramModel.TargetValType}";

                        break;
                    case RequestParamSource.NORMAL_TAG:
                        this.ParamTotalInfo = $"点名(内部):{paramModel.TagInternalName}{expressonStr}\r\n参数类型:{paramModel.TargetValType}";

                        break;
                    case RequestParamSource.MACRO:
                        this.ParamTotalInfo = $"宏名称:{paramModel.MacroName}{expressonStr}\r\n参数类型:{paramModel.TargetValType}";

                        break;
                }
            }
        }
        public ObservableCollection<RequestSpecialReturnValueModel> ReturnValueLst { get; set; }
        public ObservableCollection<ParamModel> DisplayParamLst { get; set; }
        public ObservableCollection<ParamModel> AllParamLst { get; set; }
        public ObservableCollection<ParamModel> FilterAllParamLst { get; set; }
        public ObservableCollection<DbModels.dv_request_param> ToBeAddedParamLst { get; set; }
        public ObservableCollection<DbModels.dv_request_param> ToBeDeletedParamLst { get; set; }
        public ObservableCollection<DbModels.dv_request_param> ToBeUpdateParamLst { get; set; }
        public string ParamTotalInfo { get; set; }
        public bool IsAddNew { get; set; }
        //public bool IsReturnIntArray { get; set; }
        //public bool IsReturnLong { get; set; }
        public bool IsCommReturnVal { get; set; }
        public bool IsSpecialReturnVal { get; set; }
        public bool IsSuccessShowTips { get; set; }
        public bool IsFailedShowTips { get; set; }
        public bool ShowParamEditExpander { get; set; }
        public bool IsCheckParamValidation { get; set; }
        //public bool IsSelectParam { get; set; }
        //public bool IsAddNewParam { get; set; }
        //public bool IsEditParam { get; set; }
        public ObservableCollection<string> RequestTagNameLst { get; set; }
        public ObservableCollection<DbModels.dv_tag> RequestTagLst { get; set; }
        public ObservableCollection<string> FeedbackTagNameLst { get; set; }
        public ObservableCollection<string> ArgsTagNameLst { get; set; }
        public ObservableCollection<string> EventTagNameLst { get; set; }
        public ObservableCollection<string> ReturnTagNameLst { get; set; }
        public ObservableCollection<string> ParamInternalNameLst { get; set; }
        public ObservableCollection<ParamValidationRuleModel> ParamValidationRuleLst { get; set; }
       
        public ObservableCollection<RequestParamContentModel.ValidationRule> SelectedParamValidationRuleLst { get; set; }
        public ObservableCollection<DbModels.dv_tips> DvTipsLst { get; set; }
        //参数变量
        public int ParamID { get; set; }
        public string ParameterInternalName { get; set; }
        public string ParamDesc { get; set; }
        public string ParamJsonVariableName { get; set; }
        private EnumerationMember curSelectedParamSource;
        public EnumerationMember CurSelectedParamSource
        {
            get
            {
                return curSelectedParamSource;
            }
            set
            {
                curSelectedParamSource = value;
                if(curSelectedParamSource != null&&curSelectedParamSource.Value!=null)
                {
                    this.CurParamrSource = Utli.ConvertToEnum<RequestParamSource>(curSelectedParamSource.Value.ToString());

                }
            }
        }
        private RequestParamSource curParamrSource;
        public RequestParamSource CurParamrSource
        {
            get
            {
                return curParamrSource;
            }
            set
            {
                curParamrSource = value;
                if (CurSelectedParamSource == null)
                {
                    CurSelectedParamSource = new EnumerationMember();
                    CurSelectedParamSource.Value = value.ToString();
                    CurSelectedParamSource.Description = Utli.GetEnumDescription<RequestParamSource>(value.ToString());
                }
            }
        }
        public ParamTargetType CurParamTargetType { get; set; }
        public string ConstValue { get; set; }
        //public string ControlInternalName { get; set; }
        public DbModels.dv_control_conf SelectedControl { get; set; }
        //如果是combox控件时，获取内容类型：1=index,2=Text
        public int SelectedControlComboxDataType { get; set; }
        public string TagInternalName { get; set; }
        public DbModels.dv_tag SelectedTag { get; set; }
        public string TagJsonPath { get; set; }
        public string CurMacroName { get; set; }
        public string ArrayParamIdLst { get; set; }
        public string SpliceParamIdLst { get; set; }
        public string ChildrenParamIdLst { get; set; }
        public string CurDbSheetName { get; set; }
        
       
        public ObservableCollection<RequestSpecialReturnValueModel> RequestReturnValLst { get; set; }
        public ObservableCollection<string> TagNameLst { get; set; }
        public ObservableCollection<DbModels.dv_tag> TagLst { get; set; }
        public ObservableCollection<DbModels.dv_control_conf> ControlLst { get; set; }
        public ObservableCollection<string> ControlInternalNameLst { get; set; }
        //自定义返回值
        public string ReturnValue { get; set; }
        public string ReturnValueDescZh { get; set; }
        public string ReturnValueDescEn { get; set; }
        public bool ReturnValueSuccessFlag { get; set; }
        public bool ShowCommonValEditOrAdd { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
       
        public Command ConfirmAddParameterCommand { get; set; }
        public Command ConfirmSelectParameterCommand { get; set; }
        public Command SaveEditParameterCommand { get; set; }
        public Command CancelAddParameterCommand { get; set; }

        public Command AddNewRpcParamCommand { get; set; }
        public Command SelectRpcParamCommand { get; set; }
        public Command EditRpcParamCommand { get; set; }
        public Command RemoveSelectedRpcParamCommand { get; set; }
        //自定义返回值
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        public Command AddNewCommonReturnValCommand { get; set; }
        public Command EditReturnValCommand { get; set; }
        public Command DeleteCommonReturnValCommand { get; set; }
        public Command ConfirmAddOrEditReturnValCommand { get; set; }

        public Command SelectTagCommand { get; set; }
       
        public Command ParamMultiSelectedChangeCommand { get; set; }

        public Command ConfirmChangeParameterCommand { get; set; }
        public Command ConfirmDeleteParameterCommand { get; set; }
        public Command AddNewParamValidationCommand { get; set; }
        public Command RemoveParamValidationCommand { get; set; }
        private RequestInterfaceModel curRequest;
        private DbModels.dv_system curSystem;
        #endregion
        public NewRpcEditPopupViewModel(int systemId,RequestInterfaceModel request,ECSCommType ecsComm)
        {
            curRequest = request;
            curSystem = DbHelper.db.Queryable<DbModels.dv_system>().Where(x => x.system_id == systemId).First();
          
            AllParamLst = new ObservableCollection<ParamModel>();

            var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
            foreach (var item in paramLst)
            {
                AllParamLst.Add(dvRequestSystem2ParamModel(item));
            }
            //FilterAllParamLst = new ObservableCollection<ParamModel>(AllParamLst);
            ToBeAddedParamLst = new ObservableCollection<DbModels.dv_request_param>();

            this.EcsComm = ecsComm;
            this.ParamSeperator = ",";
            RequestTagLst = new ObservableCollection<DbModels.dv_tag>(DbHelper.db.Queryable<DbModels.dv_tag>()
                .ToList()); 
            var tagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
                .Select(x => x.tag_internal_name).ToList());
            this.ArgsTagNameLst=new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("args")).ToList());
            this.EventTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("event")).ToList());
            this.ReturnTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("return")).ToList());
            RequestTagNameLst = new ObservableCollection<string>(tagNameLst.ToList());
            FeedbackTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("feedback")).ToList());
            ParamInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_request_param>()
                .Select(x => x.param_name).ToList());

            RequestSystemLst = new ObservableCollection<DbModels.dv_system>(DbHelper.db.Queryable<DbModels.dv_system>()
                .ToList());

            TagLst=new ObservableCollection<DbModels.dv_tag>(DbHelper.db.Queryable<DbModels.dv_tag>().OrderBy(x=>x.tag_internal_name).ToList());
            ParamValidationRuleLst = new ObservableCollection<ParamValidationRuleModel>();
            Type paramValidationType = typeof(RequestParamValidationType);
            FieldInfo[] fields=paramValidationType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                DescriptionAttribute attr =field.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                {
                    ParamValidationRuleLst.Add(new ParamValidationRuleModel()
                    {
                        RuleDesc = attr.Description,
                        RuleID = Convert.ToInt16(field.GetValue(null)),
                    }) ;
                }
            }
            DvTipsLst = new ObservableCollection<DbModels.dv_tips>(DbHelper.db.Queryable<DbModels.dv_tips>().ToList());
            if (request == null)
            {
                this.IsAddNew = true;
                this.IsCommReturnVal = !curSystem.is_special_return_value;
                this.IsSpecialReturnVal = curSystem.is_special_return_value;
                //this.IsReturnIntArray = true;
                this.IsWriteCmdIdIndexValueTag = true;
                this.ParamIdLst = string.Empty;
            }
            else
            {
                this.IsAddNew = false;
                this.RequestDesc = request.RequestDesc;
                this.RequestInternalName = request.RequestInternalName;
                this.RequestSystemId = request.SystemId;
                this.RequestPrecondition = request.PreCondition;
                this.ParamIdLst = request.ParamIdLst;
                this.IsFailedShowTips = request.IsFailedShowTips;
                this.IsSuccessShowTips = request.IsSuccessShowTips;
                this.EcsComm = request.EcsComm;
                if(this.EcsComm== ECSCommType.MQ)
                {
                    this.MsgType = request.MsgType;
                }
                else if(this.EcsComm== ECSCommType.OPC)
                {
                    
                    if (request.DestTagName.dest_tag_type == 1)
                    {
                        this.IsWriteSingleTag = true;
                        //this.RequestTagInternalName = request.DestTagName.request_tag_internal_name;
                        this.RequestTag = TagLst.Where(x => x.tag_internal_name == request.DestTagName.request_tag_internal_name).FirstOrDefault();
                        //this.RequestFeedbackTagInternalName = request.DestTagName.feedback_tag_internal_name;
                        this.RequestFeedbackTag = TagLst.Where(x => x.tag_internal_name == request.DestTagName.feedback_tag_internal_name).FirstOrDefault();
                    }
                    else if (request.DestTagName.dest_tag_type == 2)
                    {
                        this.IsWriteArgsEventsTag = true;
                        this.ParamSeperator = request.ParamSeparator;
                        this.ArgsTag = TagLst.Where(x => x.tag_internal_name == request.DestTagName.args_tag_internal_name).FirstOrDefault();
                        
                        this.EventTag = TagLst.Where(x => x.tag_internal_name == request.DestTagName.event_tag_internal_name).FirstOrDefault();
                       
                        this.ReturnTag = TagLst.Where(x => x.tag_internal_name == request.DestTagName.return_tag_internal_name).FirstOrDefault();
                       
                    }
                    else if (request.DestTagName.dest_tag_type == 3)
                    {
                        this.IsWriteCmdIdIndexValueTag = true;
                        this.MsgType = request.MsgType;
                        this.CmdIdTag_Dv2Plc = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_id_dv2plc_tag_internal_name).FirstOrDefault();
                        this.CmdValueTag_Dv2Plc = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_value_dv2plc_tag_internal_name).FirstOrDefault();
                        this.CmdIndexTag_Dv2Plc = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_index_dv2plc_tag_internal_name).FirstOrDefault();
                        this.CmdIdTag_Plc2Dv = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_id_plc2dv_tag_internal_name).FirstOrDefault();
                        this.CmdIndexTag_Plc2Dv = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_index_plc2dv_tag_internal_name).FirstOrDefault();
                        this.CmdValueTag_Plc2Dv = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_value_plc2dv_tag_internal_name).FirstOrDefault();
                        this.CmdReturnTag_Plc2Dv = TagLst.Where(x => x.tag_internal_name == request.DestTagName.cmd_return_plc2dv_tag_internal_name).FirstOrDefault();

                    }
                }
                //因保存基础信息后，传入的参数并未及时更新，因此返回值是通用还是自定义，需要根据数据库中获取的接口信息来判断
                this.IsCommReturnVal = !curSystem.is_special_return_value;
                this.IsSpecialReturnVal = curSystem.is_special_return_value;
                if (this.IsSpecialReturnVal&& request.ReturnValueLst!=null)
                {
                    this.ReturnValueLst = new ObservableCollection<RequestSpecialReturnValueModel>(request.ReturnValueLst);
                }
                RefreshParameterLst();

            }
            ArrayItemsParamStr = string.Empty;
            ParamMultiCheckLst = new ObservableCollection<Controls.RpcParamMultipleCheckboxModel>();
            foreach (var item in paramLst)
            {
                ParamMultiCheckLst.Add(new Controls.RpcParamMultipleCheckboxModel()
                {
                     ParamID=item.param_id,
                     ParamName=item.param_name,
                     ParamDesc=item.param_desc,
                     IsSelected=false,
                });
            }
            ParamMultiCheckLst.OrderBy(x => x.IsSelected);
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            SelectParamCommand = new Command(SelectParam);
           

            ConfirmAddParameterCommand = new Command(ConfirmAddParameter);
            ConfirmSelectParameterCommand = new Command(ConfirmSelectParameter);
            CancelAddParameterCommand = new Command(CancelAddParameter);
           
            AddNewRpcParamCommand = new Command(AddNewRpcParam);
            SelectRpcParamCommand = new Command(SelectRpcParam);
            EditRpcParamCommand = new Command(EditRpcParam);
            RemoveSelectedRpcParamCommand = new Command(RemoveSelectedParam);

            //返回值
            //AddNewCommonReturnValCommand = new Command(AddNewCommonReturnVal);
            AddNewReturnValCommand = new Command(AddNewSpecialReturnVal);
            DeleteReturnValCommand = new Command(DeleteReturnVal);
            EditReturnValCommand = new Command(EditSpecialReturnVal);
            //DeleteCommonReturnValCommand = new Command(DeleteCommonReturnVal);
            ConfirmAddOrEditReturnValCommand = new Command(ConfirmAddOrEditReturnVal);

            SelectTagCommand = new Command(SelectTag);

            ParamMultiSelectedChangeCommand = new Command((o) =>
            {
                this.ArrayParamIdLst = o.ToString();
            });
            ConfirmChangeParameterCommand = new Command(ChangeParam);
            ConfirmDeleteParameterCommand = new Command(DeleteParamItem);
            AddNewParamValidationCommand = new Command(AddNewParamValidation);
            RemoveParamValidationCommand = new Command(RemoveParamValidation);
        }
        /// <summary>
        /// 添加参数校验规则
        /// </summary>
        /// <param name="o"></param>
        public void AddNewParamValidation(object o)
        {
            if (this.SelectedParamValidationRuleLst == null)
            {
                this.SelectedParamValidationRuleLst = new ObservableCollection<RequestParamContentModel.ValidationRule>();
            }
            if (this.SelectedParamValidationRuleLst.Count(x => x.validation_type_id == 0 || string.IsNullOrEmpty(x.invalid_tips_internal_name))>0)
            {
                MessageBox.Show("请先选择校验规则、失败弹窗！"); return;
            }
            this.SelectedParamValidationRuleLst.Add(new RequestParamContentModel.ValidationRule()
            {

            });
        }
        /// <summary>
        /// 移除参数校验规则
        /// </summary>
        /// <param name="o"></param>
        public void RemoveParamValidation(object o)
        {
            var selectedValidationTypeId = Convert.ToInt16(o);
            if(this.SelectedParamValidationRuleLst!=null&& this.SelectedParamValidationRuleLst.Count() > 0)
            {
                var validationItem=this.SelectedParamValidationRuleLst.Where(x=>x.validation_type_id==selectedValidationTypeId).FirstOrDefault();
                if (validationItem != null)
                {
                    this.SelectedParamValidationRuleLst.Remove(validationItem);
                }
            }
        }
        /// <summary>
        /// 确认修改参数
        /// </summary>
        /// <param name="o"></param>
        public void ChangeParam(object o)
        {
            try
            {
                DataView_Configuration.RequestParamContentModel rp = new RequestParamContentModel();
                switch (this.CurParamrSource)
                {
                    case RequestParamSource.CONTROL:
                        if (this.SelectedControl == null)
                        {
                            MessageBox.Show("请选择控件！"); return;
                        }
                        rp.control_internal_name = this.SelectedControl?.dv_control_internal_name;
                        rp.control_combox_data_type = this.SelectedControlComboxDataType;
                        rp.value_expression = this.ParamExpressionStr;
                        break;
                    case RequestParamSource.CONSTANT:
                        rp.constant_value = this.ConstValue;

                        break;
                    case RequestParamSource.MACRO:
                        if (string.IsNullOrEmpty(CurMacroName))
                        {
                            MessageBox.Show("请输入宏的名称！"); return;
                        }
                        rp.macro_name = this.CurMacroName;
                        rp.value_expression = this.ParamExpressionStr;
                        break;
                    case RequestParamSource.NORMAL_TAG:
                        if (this.SelectedTag == null)
                        {
                            MessageBox.Show("请选择点名！"); return;
                        }
                        rp.tag_internal_name = this.SelectedTag.tag_internal_name;
                        rp.value_expression = this.ParamExpressionStr;
                        break;
                    case RequestParamSource.JSON_TAG:
                        if (this.SelectedTag == null)
                        {
                            MessageBox.Show("请选择点名！"); return;
                        }
                        else if (string.IsNullOrEmpty(this.TagJsonPath))
                        {
                            MessageBox.Show("请选择输入JSON字段路径！"); return;
                        }
                        rp.tag_internal_name = this.SelectedTag.tag_internal_name;
                        rp.tag_value_json_path = this.TagJsonPath;
                        rp.value_expression = this.ParamExpressionStr;
                        break;
                    case RequestParamSource.ARRAY_ONE_ITEM:
                        rp.array_param_id_list = this.ArrayParamIdLst;
                        break;
                }
                //校验规则添加
                rp.param_validation_enable = this.IsCheckParamValidation;
                if (rp.param_validation_enable)
                {
                    if (this.SelectedParamValidationRuleLst.Count(x => x.validation_type_id == 0 || string.IsNullOrEmpty(x.invalid_tips_internal_name)) > 0)
                    {
                        MessageBox.Show("请先选择校验规则、失败弹窗！"); return;
                    }
                    rp.validation_rules = new List<RequestParamContentModel.ValidationRule>(this.SelectedParamValidationRuleLst);
                }
                //rp.control_internal_name = this.SelectedControl?.dv_control_internal_name;
                //rp.macro_name = this.CurMacroName;
                //rp.constant_value = this.ConstValue;
                //rp.tag_internal_name = this.SelectedTag?.tag_internal_name;// this.TagInternalName;
                //rp.tag_value_json_path = this.TagJsonPath;
                //rp.array_param_id_list = this.ArrayParamIdLst;
                //rp.db_sheet_name = this.CurDbSheetName;
                //rp.splice_param_id_list = this.SpliceParamIdLst;
                DbModels.dv_request_param c = new DbModels.dv_request_param()
                {
                    param_id = SelectedParam.ParamID,
                    param_name = SelectedParam.ParamName,
                    param_json_variable_name = string.IsNullOrEmpty(this.ParamJsonVariableName) ? "default" : this.ParamJsonVariableName,
                    param_source = (int)this.CurParamrSource,
                    param_content = JsonConvert.SerializeObject(rp),
                    value_type = this.CurParamTargetType.ToString(),
                    param_desc = this.ParamDesc,


                };
                var affectedRow = DbHelper.db.Updateable<DbModels.dv_request_param>(c).Where(x => x.param_id == c.param_id).ExecuteCommand();
                if (affectedRow > 0)
                {
                    MessageBox.Show("修改成功！");
                    this.ShowParamEditExpander = false;

                    AllParamLst = new ObservableCollection<ParamModel>();

                    var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
                    foreach (var item in paramLst)
                    {
                        AllParamLst.Add(dvRequestSystem2ParamModel(item));
                    }
                    FilterAllParamLst = new ObservableCollection<ParamModel>(AllParamLst);
                    //数组选择子参数

                    ArrayItemsParamStr = string.Empty;
                    ParamMultiCheckLst = new ObservableCollection<Controls.RpcParamMultipleCheckboxModel>();
                    foreach (var item in paramLst)
                    {
                        ParamMultiCheckLst.Add(new Controls.RpcParamMultipleCheckboxModel()
                        {
                            ParamID = item.param_id,
                            ParamName = item.param_name,
                            ParamDesc = item.param_desc,
                            IsSelected = false,
                        });
                    }
                    //刷新显示的参数列表
                    RefreshParameterLst();
                    return;
                }
                else
                {
                    MessageBox.Show("修改失败！");

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("修改失败！" + ex.ToString());
                LogHelper.Error($"[ChangeParam]{ex.ToString()}");
            }
        }
        public void RemoveSelectedParam(object o)
        {
            ParamModel paramModel = o as ParamModel;

            DisplayParamLst.Remove(paramModel);
        }
        public void DeleteParamItem(object o)
        {
            if (MessageBox.Show("确认要删除该参数吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var existedParamUsedInterfaceLst = DbHelper.db.Queryable<DbModels.dv_request_interface>()
                .Where(x => x.request_param_list.Contains(this.ParamID.ToString())).ToList();
            if (existedParamUsedInterfaceLst.Count()>0)
            {
                var displayInterfaceStr = string.Empty;
                foreach (var item in existedParamUsedInterfaceLst)
                {
                    displayInterfaceStr += item.request_internal_name + ":" + item.request_desc + "\r\n";
                }
                MessageBox.Show($"已有如下RPC接口使用该参数，请调整好接口参数后，再进行删除！\r\n{displayInterfaceStr}");
                return;
            }
            //ParamChange = ParamChangeType.EditExist;
            var  affectedRow=DbHelper.db.Deleteable<DbModels.dv_request_param>().Where(x => x.param_id == this.ParamID).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("删除成功！");
                this.ShowParamEditExpander = false;
                this.SelectedParam = null;
                AllParamLst = new ObservableCollection<ParamModel>();

                var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
                foreach (var item in paramLst)
                {
                    AllParamLst.Add(dvRequestSystem2ParamModel(item));
                }
                FilterAllParamLst = new ObservableCollection<ParamModel>(AllParamLst);
                //数组选择子参数
                ArrayItemsParamStr = string.Empty;
                ParamMultiCheckLst = new ObservableCollection<Controls.RpcParamMultipleCheckboxModel>();
                foreach (var item in paramLst)
                {
                    ParamMultiCheckLst.Add(new Controls.RpcParamMultipleCheckboxModel()
                    {
                        ParamID = item.param_id,
                        ParamName = item.param_name,
                        ParamDesc=item.param_desc,
                        IsSelected = false,
                    });
                }
                return;
            }
            else
            {
                MessageBox.Show("删除失败！");
               
            }
        }
        public void UpdateParamItems(string str)
        {
            FilterAllParamLst = new ObservableCollection<ParamModel>(AllParamLst.Where(x => x.ParamName.Contains(str)).ToList());
        }
        private void SelectTag(object o)
        {
            var tagName = "";
            var selectedNewTagInternalName = "";
            switch (o.ToString().ToLower())
            {
                case "direct_write_tag":
                    tagName = this.RequestTag.tag_internal_name;// RequestTagInternalName;
                    break;
                case "direct_write_tag_feedback":
                    tagName = this.RequestFeedbackTag.tag_internal_name;// RequestFeedbackTagInternalName;
                    break;
                case "args_tag":
                    tagName = ArgsTagName;
                    break;
                case "event_tag":
                    tagName = EventTagName;
                    break;
                case "return_tag":
                    tagName = ReturnTagName;
                    break; 
                case "param_tag":
                    tagName = SelectedTag.tag_internal_name;// TagInternalName;
                    break;
            }
            TagSelectPopup ce = new TagSelectPopup(tagName);
            //MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                selectedNewTagInternalName =(ce.DataContext as ViewModels.TagSelectPopupViewModel).SelectedTagInternalName;
                switch (o.ToString().ToLower())
                {
                    case "direct_write_tag":
                        this.RequestTagInternalName = selectedNewTagInternalName;
                        break;
                    case "direct_write_tag_feedback":
                        this.RequestFeedbackTagInternalName = selectedNewTagInternalName; 
                        break;
                    case "args_tag":
                        ArgsTagName = selectedNewTagInternalName;
                        break;
                    case "event_tag":
                        EventTagName = selectedNewTagInternalName;
                        break;
                    case "return_tag":
                        ReturnTagName = selectedNewTagInternalName;
                        break;
                    case "param_tag":
                        SelectedTag = TagLst.Where(x => x.tag_internal_name == selectedNewTagInternalName).FirstOrDefault();
                        //TagInternalName = selectedNewTagInternalName;
                        break;
                }
            }
            //MainWindow.RemoveMask();
            
        }
        private void AddNewSpecialReturnVal(object o)
        {
            if (ShowCommonValEditOrAdd)
            {
                ShowCommonValEditOrAdd = false; return;
            }
            var t = o as DbModels.dv_request_return_code;
            this.ReturnValue = string.Empty;
            this.ReturnValueDescZh = string.Empty;
            this.ReturnValueDescEn = string.Empty;
            this.ReturnValueSuccessFlag = false;
            ShowCommonValEditOrAdd = true;
            IsEditExistReturnVal = false;
        }
        private bool IsEditExistReturnVal = false;
      
        private void EditSpecialReturnVal(object o)
        {
            var t = o as RequestSpecialReturnValueModel;

            this.ReturnValue = t.return_value;
            this.ReturnValueDescZh = t.return_desc_zh;
            this.ReturnValueDescEn = t.return_desc_en;
            this.ReturnValueSuccessFlag = t.is_success_flag;
            ShowCommonValEditOrAdd = true;
            IsEditExistReturnVal = true;
        }
        private void DeleteCommonReturnVal(object o)
        {
            var selectedCrane = o as RequestSpecialReturnValueModel;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            //if (MessageBox.Show("确认要删除该返回值吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            //var iRet = DbHelper.db.Deleteable<DbModels.dv_request_return_code>()
            //    .Where(x => x.id == selectedCrane.id && x.system_id == m_SystemId).ExecuteCommand();
            ReturnValueLst.Remove(selectedCrane);
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void ConfirmAddOrEditReturnVal(object o)
        {
            if (string.IsNullOrEmpty(this.ReturnValue))
            {
                MessageBox.Show("返回值不能为空！"); return;
            }
            if (!IsEditExistReturnVal)
            {
                if (this.ReturnValueLst == null)
                {
                    this.ReturnValueLst = new ObservableCollection<RequestSpecialReturnValueModel>();
                }
                if (new List<RequestSpecialReturnValueModel>(this.ReturnValueLst).Exists(x => x.return_value == this.ReturnValue))
                {
                    MessageBox.Show("返回值不能重复！"); return;
                }
                
            }
           
            var existedReutrnVal= ReturnValueLst.Where(x=>x.return_value==this.ReturnValue).FirstOrDefault();
            if (existedReutrnVal != null)
            {
                var iIndex = ReturnValueLst.IndexOf(existedReutrnVal);
                ReturnValueLst[iIndex].is_success_flag = this.ReturnValueSuccessFlag;
                ReturnValueLst[iIndex].return_desc_en = this.ReturnValueDescEn;
                ReturnValueLst[iIndex].return_desc_zh = this.ReturnValueDescZh;
            }
            else
            {
                this.ReturnValueLst.Add(new RequestSpecialReturnValueModel()
                {
                    is_success_flag = this.ReturnValueSuccessFlag,
                    return_desc_en = this.ReturnValueDescEn,
                    return_desc_zh = this.ReturnValueDescZh,
                    return_value = this.ReturnValue,
                });
            }
            var tmpLst = this.ReturnValueLst;
            this.ReturnValueLst = null;
            this.ReturnValueLst = tmpLst;
            ShowCommonValEditOrAdd = false;
            
        }
        
        /// <summary>
        /// 来自数据库的request param转换为配置工具内部的param model
        /// </summary>
        /// <param name="dvRequestParam"></param>
        /// <returns></returns>
        private ParamModel dvRequestSystem2ParamModel(DbModels.dv_request_param dvRequestParam)
        {
            var paramContent = JsonConvert.DeserializeObject<DataView_Configuration.RequestParamContentModel>(dvRequestParam.param_content);
            var pm = new ParamModel()
            {
                ParamID = dvRequestParam.param_id,
                ParamName = dvRequestParam.param_name,
                ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), dvRequestParam.param_source.ToString(), true),
                JsonVariableName = dvRequestParam.param_json_variable_name,
                ExpressionStr = paramContent.value_expression,
                TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), dvRequestParam.value_type, true),
                ControlInternalName = paramContent.control_internal_name == null ? "" : paramContent.control_internal_name,
                ControlComboxIndexOrTextType = paramContent.control_combox_data_type,
                TagInternalName = paramContent.tag_internal_name == null ? "" : paramContent.tag_internal_name,
                TagValueJsonPath = paramContent.tag_value_json_path == null ? "" : paramContent.tag_value_json_path,
                ConstValue = paramContent.constant_value == null ? "" : paramContent.constant_value,
                MacroName = paramContent.macro_name == null ? "" : paramContent.macro_name,
                ReportColumnName = paramContent.report_column_name == null ? "" : paramContent.report_column_name,
                ArrayParamIdLst = paramContent.array_param_id_list == null ? "" : paramContent.array_param_id_list,
                SpliceParamIdLst = paramContent.splice_param_id_list == null ? "" : paramContent.splice_param_id_list,
                DbSheetName = paramContent.db_sheet_name == null ? "" : paramContent.db_sheet_name,
                ChildrenParamId = paramContent.children_param_id == null ? "" : paramContent.children_param_id,
                ParamDesc = dvRequestParam.param_desc,
                IsParamValidationCheck = paramContent.param_validation_enable,
                ValidationRuleLst = paramContent.validation_rules,
            };
            return pm;
        }/// <summary>
         /// 配置工具内部的param model转换为来自数据库的request param
         /// </summary>
         /// <param name="dvRequestParam"></param>
         /// <returns></returns>
        private DbModels.dv_request_param ParamModel2dvRequestSystem(ParamModel paramModel)
        {
            var paramContent = new RequestParamContentModel()
            {
                control_internal_name = paramModel.ControlInternalName,
                tag_internal_name=paramModel.TagInternalName,
                tag_value_json_path=paramModel.TagValueJsonPath,
                constant_value = paramModel.ConstValue,
                macro_name= paramModel.MacroName,
                report_column_name= paramModel.ReportColumnName,
                array_param_id_list = paramModel.ArrayParamIdLst,
                splice_param_id_list = paramModel.SpliceParamIdLst,
                db_sheet_name = paramModel.DbSheetName,
                children_param_id = paramModel.ChildrenParamId,
                param_validation_enable=paramModel.IsParamValidationCheck,
                validation_rules=paramModel.ValidationRuleLst,
                value_expression= paramModel.ExpressionStr,
            };            
            var dvRequestParam = new DbModels.dv_request_param()
            {
                param_id = paramModel.ParamID,
                param_name=paramModel.ParamName,
                param_source= (int)paramModel.ParamSource,
                param_json_variable_name= paramModel.JsonVariableName,
                value_type= paramModel.TargetValType.ToString(),
                param_desc = paramModel.ParamDesc,
                param_content= JsonConvert.SerializeObject(paramContent),
            };
            return dvRequestParam;
        }
        private void RefreshParameterLst(string searchTxt = null)
        {
            if (string.IsNullOrEmpty(ParamIdLst)) return;
            var paramArray = ParamIdLst.Split(',');
            DisplayParamLst = new ObservableCollection<ParamModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_request_param>()
                .LeftJoin<DbModels.dv_request_param_source>((o, cus) => o.param_source == cus.source_id)
                .Where(o=> SqlFunc.ContainsArray<string>(paramArray,o.param_id.ToString()))
                .ToList();
            for (int i = 0; i < paramArray.Length; i++)
            {
                var tmpReq = curRequests.Where(x => x.param_id.ToString() == paramArray[i]).First();
                if (tmpReq == null) continue;
                var pm = dvRequestSystem2ParamModel(tmpReq);

                //模糊搜索，参数名称，json变量名称，参数描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    DisplayParamLst.Add(pm);
                }
                else
                {
                    if (Utli.StringContains(pm.ControlInternalName, searchTxt)
                         || Utli.StringContains(pm.JsonVariableName, searchTxt)
                         || Utli.StringContains(pm.ParamDesc, searchTxt)

                         )
                    {
                        DisplayParamLst.Add(pm);
                    }
                }
            }
            //foreach (var item in curRequests)
            //{
            //    var pm =dvRequestSystem2ParamModel(item);
            //    //var paramContent = JsonConvert.DeserializeObject<DataView_Configuration.RequestParamContentModel>(item.param_content);
            //    //var pm = new ParamModel()
            //    //{
            //    //    Id = item.param_id,
            //    //    ParamInternalName = item.param_name,
            //    //    ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), item.param_source.ToString(), true),
            //    //    ParamJsonVariableName = item.param_json_variable_name,
            //    //    TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), item.value_type, true),
            //    //    ControlInternalName = paramContent.control_internal_name == null ? "" : paramContent.control_internal_name,
            //    //    TagInternalName = paramContent.tag_internal_name == null ? "" : paramContent.tag_internal_name,
            //    //    TagValueJsonPath = paramContent.tag_value_json_path == null ? "" : paramContent.tag_value_json_path,
            //    //    ConstValue = paramContent.constant_value == null ? "" : paramContent.constant_value,
            //    //    MacroName = paramContent.macro_name == null ? "" : paramContent.macro_name,
            //    //    ReportColumnName = paramContent.report_column_name == null ? "" : paramContent.report_column_name,
            //    //    ArrayParamIdLst = paramContent.array_param_id_list == null ? "" : paramContent.array_param_id_list,
            //    //    SpliceParamIdLst = paramContent.splice_param_id_list == null ? "" : paramContent.splice_param_id_list,
            //    //    DbSheetName = paramContent.db_sheet_name == null ? "" : paramContent.db_sheet_name,
            //    //    ChildrenParamId = paramContent.children_param_id == null ? "" : paramContent.children_param_id,
            //    //    ParamDesc = item.param_desc,
            //    //};

            //    //模糊搜索，参数名称，json变量名称，参数描述
            //    if (string.IsNullOrEmpty(searchTxt))
            //    {
            //        DisplayParamLst.Add(pm);
            //    }
            //    else
            //    {
            //        if (Utli.StringContains(pm.ControlInternalName, searchTxt)
            //             || Utli.StringContains(pm.ParamJsonVariableName, searchTxt)
            //             || Utli.StringContains(pm.ParamDesc, searchTxt)

            //             )
            //        {
            //            DisplayParamLst.Add(pm);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 根据参数列表获取param id list
        /// </summary>
        /// <returns></returns>
        private string GetParamIdLst()
        {
            var paramIdLst = "";
            if (DisplayParamLst == null || DisplayParamLst.Count == 0) return "";
            foreach (var item in DisplayParamLst)
            {
                if (string.IsNullOrEmpty(paramIdLst))
                {
                    paramIdLst += item.ParamID;
                }
                else
                {
                    paramIdLst += "," + item.ParamID;
                }
            }
            return paramIdLst;
        }
        /// <summary>
        /// 选择已有参数
        /// </summary>
        /// <param name="o"></param>
        private void SelectRpcParam(object o)
        {
            if (this.ShowParamEditExpander)
            {
                this.ShowParamEditExpander = false; return;
            }
            TagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
               .Select(x => x.tag_internal_name).ToList());
            ControlInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
                .Select(x => x.dv_control_internal_name).ToList());
            ControlLst = new ObservableCollection<DbModels.dv_control_conf>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
               .ToList());
            this.ParamID = 0;
            this.ShowParamEditExpander = true;
            this.ParameterInternalName = "";
            this.ParamDesc = "";
            ParamChange = ParamChangeType.SelectOther;
        }
        /// <summary>
        /// 添加新参数
        /// </summary>
        /// <param name="o"></param>
        private void AddNewRpcParam(object o)
        {
            if (this.ShowParamEditExpander)
            {
                //if (this.IsAddNewParam)
                //{
                //    this.IsAddNewParam = false;
                //    this.IsSelectParam = true;
                //}
                this.ShowParamEditExpander = false;return;
            }
            TagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
               .Select(x => x.tag_internal_name).ToList());
            ControlInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
                .Select(x => x.dv_control_internal_name).ToList());
            ControlLst = new ObservableCollection<DbModels.dv_control_conf>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
               .ToList());
            this.ParamID = 0;
            this.ShowParamEditExpander = true;
            this.ParameterInternalName = "";
            this.ParamDesc = "";
            ParamChange = ParamChangeType.AddNew;
        }
        /// <summary>
        /// 编辑参数
        /// </summary>
        /// <param name="o"></param>
        private void EditRpcParam(object o)
        {
            try
            {
                var paramModel = o as ParamModel;
                this.ShowParamEditExpander = true;
                ParamChange = ParamChangeType.EditExist;
                this.ParameterInternalName = paramModel.ParamName;
                //TagLst = new ObservableCollection<DbModels.dv_tag>(DbHelper.db.Queryable<DbModels.dv_tag>().OrderBy(x => x.tag_internal_name).ToList());
                //TagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
                //    .Select(x => x.tag_internal_name).ToList());
                ControlInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
                    .Select(x => x.dv_control_internal_name).ToList());
                ControlLst = new ObservableCollection<DbModels.dv_control_conf>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
                  .ToList());
                this.ParamID = paramModel.ParamID;
                this.ParameterInternalName = paramModel.ParamName;
                this.CurParamrSource = paramModel.ParamSource;
                this.ParamJsonVariableName = paramModel.JsonVariableName;
                this.ParamExpressionStr = paramModel.ExpressionStr;
                this.ConstValue = paramModel.ConstValue;
                this.CurMacroName = paramModel.MacroName;
                this.CurDbSheetName = paramModel.DbSheetName;
                this.SelectedControl = ControlLst?.Where(x => x.dv_control_internal_name == paramModel.ControlInternalName).FirstOrDefault();

                //this.TagInternalName = paramModel.TagInternalName;
                this.SelectedTag = TagLst.Where(x => x.tag_internal_name == paramModel.TagInternalName).FirstOrDefault();
                this.TagJsonPath = paramModel.TagValueJsonPath;
                this.ArrayParamIdLst = paramModel.ArrayParamIdLst;
                this.CurParamTargetType = paramModel.TargetValType;
                this.ParamDesc = paramModel.ParamDesc;
                if (paramModel.ReturnValueLst != null)
                {
                    this.RequestReturnValLst = new ObservableCollection<RequestSpecialReturnValueModel>(paramModel.ReturnValueLst);
                }
                else
                {
                    this.RequestReturnValLst = new ObservableCollection<RequestSpecialReturnValueModel>();
                }
                //校验规则
                this.IsCheckParamValidation = paramModel.IsParamValidationCheck;
                if (this.IsCheckParamValidation)
                {

                    this.SelectedParamValidationRuleLst = new ObservableCollection<RequestParamContentModel.ValidationRule>(paramModel.ValidationRuleLst);
                }
                this.SelectedParam = paramModel;
                ArrayItemsParamStr = string.Empty;
                var tmpArray = this.ArrayParamIdLst.Split(',');
                foreach (var item in ParamMultiCheckLst)
                {
                    if (tmpArray.Contains(item.ParamID.ToString()))
                    {
                        item.IsSelected = true;
                        ArrayItemsParamStr += item.ParamName + ",";
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                }
                ParamMultiCheckLst=new ObservableCollection<Controls.RpcParamMultipleCheckboxModel>(ParamMultiCheckLst.OrderByDescending(x => x.IsSelected).ToList());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("打开编辑页面失败！" + ex.ToString());
                LogHelper.Error($"[EditRpcParam]{ex.ToString()}");
            }
        }
        /// <summary>
        /// 删除rpc参数（缓存）
        /// </summary>
        /// <param name="o"></param>
        private void DeleteRpcParam(object o)
        {
            if (MessageBox.Show("确认要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var paramModel = o as ParamModel;
            if (DisplayParamLst == null || DisplayParamLst.Count == 0)
            {
                return;
            }
            if (ToBeDeletedParamLst == null)
            {
                ToBeDeletedParamLst = new ObservableCollection<DbModels.dv_request_param>();
            }
            //如果param id不为0，则直接删掉;如果param id为0，则仅删除ToBeAddedParamlst中的元素
            //id=0,表示仅存在程序缓存，没有存到数据库中去
            if (paramModel.ParamID == 0)
            {
                var param = this.ToBeAddedParamLst.Where(x => x.param_name == paramModel.ParamName).First();
                this.ToBeAddedParamLst.Remove(param);
            }
            else
            {
                //如果待删除列表中不包含该参数，则添加
                if (!new List<DbModels.dv_request_param>(this.ToBeDeletedParamLst).Exists(x => x.param_id == paramModel.ParamID))
                {
                    this.ToBeDeletedParamLst.Add(ParamModel2dvRequestSystem(paramModel));
                }
            }
            this.DisplayParamLst.Remove(paramModel);
            if (this.ParamIdLst != null)
            {
                this.ParamIdLst.Remove(paramModel.ParamID);
            }
        }
        private void DeleteReturnVal(object o)
        {
            var returnVal = o as RequestSpecialReturnValueModel;
            if (ReturnValueLst == null || ReturnValueLst.Count == 0)
            {
                return;
            }
            this.ReturnValueLst.Remove(returnVal);
        }
        //private void AddNewReturnVal(object o)
        //{
        //    if (ReturnValueLst == null || ReturnValueLst.Count == 0)
        //    {
        //        this.ReturnValueLst = new ObservableCollection<RequestSpecialReturnValueModel>();
        //    }
        //    this.ReturnValueLst.Add(new RequestSpecialReturnValueModel()
        //    {
        //        return_value="1",
        //        return_desc="test",
        //    });
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">参数列表</param>
        private void SelectParam(object o)
        {
            short[] paramIdArray = null;
            if (o!=null&&!string.IsNullOrEmpty(o.ToString()))
            {
               paramIdArray = Array.ConvertAll<string, short>(o.ToString().Split(','), s => Convert.ToInt16(s));
            }
            Pages.Popups.ParamSelectedPopup paramSelectPopup = new Pages.Popups.ParamSelectedPopup(paramIdArray);
            var bRet=paramSelectPopup.ShowDialog();
            if (bRet.Value)
            {
                this.ParamIdLst =
               String.Join(",", (paramSelectPopup.DataContext as ViewModels.ParamSelectedPopupViewModel).paramIdLst.ToArray());
            }
        }
        /// <summary>
        /// 确认选择的参数
        /// </summary>
        /// <param name="o"></param>
        private void ConfirmSelectParameter(object o)
        {           
            ParamModel paramModel = new ParamModel();
            if (this.SelectedParam==null)
            {
                MessageBox.Show("参数不能为空！"); return;
            }
            if (string.IsNullOrEmpty(this.ParamJsonVariableName))
            {
                this.ParamJsonVariableName = "default";
            }
            DataView_Configuration.RequestParamContentModel rp = new RequestParamContentModel();
            rp.control_internal_name = this.SelectedControl?.dv_control_internal_name;
            rp.control_combox_data_type = this.SelectedControlComboxDataType;
            rp.macro_name = this.CurMacroName;
            rp.constant_value = this.ConstValue;
            rp.tag_internal_name = this.TagInternalName;
            rp.tag_value_json_path = this.TagJsonPath;
            rp.array_param_id_list = this.ArrayParamIdLst;
            rp.db_sheet_name = this.CurDbSheetName;
            rp.splice_param_id_list = this.SpliceParamIdLst;
            rp.value_expression = this.ParamExpressionStr;
            DbModels.dv_request_param c = new DbModels.dv_request_param()
            {
                param_id = SelectedParam.ParamID,
                param_name = SelectedParam.ParamName,
                param_json_variable_name = string.IsNullOrEmpty(this.ParamJsonVariableName) ? "default" : this.ParamJsonVariableName,
                param_source = (int)this.CurParamrSource,
                param_content = JsonConvert.SerializeObject(rp),
                value_type = this.CurParamTargetType.ToString(),
                param_desc = this.ParamDesc,
            };
            var affectedRow = DbHelper.db.Updateable<DbModels.dv_request_param>(c).Where(x => x.param_id == c.param_id);
            if (this.DisplayParamLst == null)
            {
                DisplayParamLst = new ObservableCollection<ParamModel>();
            }
            this.DisplayParamLst.Add(dvRequestSystem2ParamModel(c));
            
            this.ShowParamEditExpander = false;
        }
        /// <summary>
        /// 确认添加参数
        /// </summary>
        /// <param name="o"></param>
        private void ConfirmAddParameter(object o)
        {
            ParamModel paramModel = new ParamModel();
            if (string.IsNullOrEmpty(ParameterInternalName))
            {
                MessageBox.Show("参数名称不能为空！"); return;
            }
            if (string.IsNullOrEmpty(this.ParamJsonVariableName))
            {
                this.ParamJsonVariableName = "default";
            }
            DataView_Configuration.RequestParamContentModel rp = new RequestParamContentModel();
            switch (this.CurParamrSource)
            {
                case RequestParamSource.CONTROL:
                    if (this.SelectedControl == null)
                    {
                        MessageBox.Show("请选择控件！"); return;
                    }
                    rp.control_internal_name = this.SelectedControl?.dv_control_internal_name;
                    rp.control_combox_data_type = this.SelectedControlComboxDataType;
                    rp.value_expression = this.ParamExpressionStr;
                    break;
                case RequestParamSource.CONSTANT:
                    rp.constant_value = this.ConstValue;
                    break;
                    //rp.value_expression = this.ParamExpressionStr;
                case RequestParamSource.MACRO:
                    if (string.IsNullOrEmpty(CurMacroName))
                    {
                        MessageBox.Show("请输入宏的名称！"); return;
                    }
                    rp.macro_name = this.CurMacroName;
                    rp.value_expression = this.ParamExpressionStr;
                    break;
                case RequestParamSource.NORMAL_TAG:
                    if (this.SelectedTag==null)
                    {
                        MessageBox.Show("请选择点名！"); return;
                    }
                    rp.tag_internal_name = this.SelectedTag.tag_internal_name;
                    rp.value_expression = this.ParamExpressionStr;
                    break;
                case RequestParamSource.JSON_TAG:
                    if (this.SelectedTag == null)
                    {
                        MessageBox.Show("请选择点名！"); return;
                    }
                    else if (string.IsNullOrEmpty(this.TagJsonPath))
                    {
                        MessageBox.Show("请选择输入JSON字段路径！"); return;
                    }
                    rp.tag_internal_name = this.SelectedTag.tag_internal_name;
                    rp.tag_value_json_path = this.TagJsonPath;
                    rp.value_expression = this.ParamExpressionStr;
                    break;
                case RequestParamSource.ARRAY_ONE_ITEM:
                    rp.array_param_id_list = this.ArrayParamIdLst;
                    break;
            }
            //校验规则
            rp.param_validation_enable = this.IsCheckParamValidation;
            if (rp.param_validation_enable)
            {
                rp.validation_rules = new List<RequestParamContentModel.ValidationRule>(this.SelectedParamValidationRuleLst);
            }
            //rp.db_sheet_name = this.CurDbSheetName;
            //rp.splice_param_id_list = this.SpliceParamIdLst;
            DbModels.dv_request_param c = new DbModels.dv_request_param()
            {
                //param_id = this.ParamID,自增
                param_name = this.ParameterInternalName,
                param_json_variable_name = string.IsNullOrEmpty(this.ParamJsonVariableName) ? "default" : this.ParamJsonVariableName,
                param_source = (int)this.CurParamrSource,
                param_content = JsonConvert.SerializeObject(rp),
                value_type = this.CurParamTargetType.ToString(),
                param_desc = this.ParamDesc,
            };
            if (AllParamLst.Any(x=>x.ParamName== c.param_name))
            {
                MessageBox.Show("参数名称重复，请修改名称！");return;
            }
            var addedParamId = DbHelper.db.Insertable<DbModels.dv_request_param>(c).ExecuteReturnIdentity();
            if (addedParamId > 0)
            {
                MessageBox.Show("添加成功！");
                c.param_id = addedParamId;
                //AllParamLst = new ObservableCollection<ParamModel>();
                AllParamLst.Add(dvRequestSystem2ParamModel(c));
                //var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
                //foreach (var item in paramLst)
                //{
                //    AllParamLst.Add(dvRequestSystem2ParamModel(item));
                //}
                ArrayItemsParamStr = string.Empty;
                ParamMultiCheckLst.Add(new Controls.RpcParamMultipleCheckboxModel()
                {
                    ParamID = c.param_id,
                    ParamName = c.param_name,
                    IsSelected = false,
                });
                FilterAllParamLst = new ObservableCollection<ParamModel>(AllParamLst);
                //数组选择子参数
                //ParamMultiCheckLst = new ObservableCollection<Controls.RpcParamMultipleCheckboxModel>();
                //foreach (var item in paramLst)
                //{
                //    ParamMultiCheckLst.Add(new Controls.RpcParamMultipleCheckboxModel()
                //    {
                //        ParamID = item.param_id,
                //        ParamName = item.param_name,
                //        IsSelected = false,
                //    });
                //}
                this.DisplayParamLst.Add(dvRequestSystem2ParamModel(c));
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
            //if (ParamChange == ParamChangeType.AddNew)
            //{
            //    //
            //    this.ToBeAddedParamLst.Add(c);
            //    this.DisplayParamLst.Add(dvRequestSystem2ParamModel(c));
            //}
            //else if (ParamChange == ParamChangeType.EditExist)
            //{
            //    if (this.ToBeUpdateParamLst == null) this.ToBeUpdateParamLst = new ObservableCollection<DbModels.dv_request_param>();
            //    this.ToBeUpdateParamLst.Add(c);
            //    var curParamModel = this.DisplayParamLst.Where(x => x.ParamInternalName == ParameterInternalName).First();
            //    this.DisplayParamLst.Remove(curParamModel);
            //    this.DisplayParamLst.Add(dvRequestSystem2ParamModel(c));

            //}
            this.ShowParamEditExpander = false;
        }
        private void CancelAddParameter(object o)
        {
            this.ShowParamEditExpander = false;
        }
        //接口编辑 确认
        private void Confirm(object o)
        {
            try
            {
                //check
                if (string.IsNullOrEmpty(this.RequestInternalName))
                {
                    System.Windows.MessageBox.Show("请输入接口名称！");return; ;
                }
                DataView_Configuration.RequestDestTagModel rd= new RequestDestTagModel(); 
                if (this.EcsComm == ECSCommType.OPC )
                {
                    if (this.IsSpecialReturnVal && (this.ReturnValueLst == null || this.ReturnValueLst.Count == 0))
                    {
                        System.Windows.MessageBox.Show("当前选择的接口交互类型，不支持单独定义返回值，请选择通用返回值！"); return;
                    }
                    //写点类型
                    var curSelectedWriteTagType = 1;

                    //写单个点并等待返回
                    if (this.IsWriteSingleTag)
                    {
                        if (this.RequestTag==null||this.RequestFeedbackTag == null)
                        {
                            System.Windows.MessageBox.Show("请选择点位！"); return;
                        }
                        curSelectedWriteTagType = 1;
                    }
                    //写args,event,等待return
                    else if(this.IsWriteArgsEventsTag)
                    {
                        if (this.EventTag==null||this.ArgsTag == null || this.ReturnTag == null)
                        {
                            System.Windows.MessageBox.Show("请选择点位！"); return;
                        }
                        curSelectedWriteTagType = 2;
                    }
                    //写cmdId/Index/Value
                    else if (this.IsWriteCmdIdIndexValueTag)
                    {
                        if(this.CmdIdTag_Dv2Plc==null||this.CmdIndexTag_Dv2Plc==null||this.CmdValueTag_Dv2Plc == null
                            || this.CmdIdTag_Plc2Dv == null || this.CmdIndexTag_Plc2Dv == null || this.CmdValueTag_Plc2Dv == null 
                            || this.CmdReturnTag_Plc2Dv == null)
                        {
                            System.Windows.MessageBox.Show("请选择点位！"); return;
                        }
                        curSelectedWriteTagType = 3;
                    }
                    
                    rd = new RequestDestTagModel();
                    rd.dest_tag_type = curSelectedWriteTagType;
                    rd.request_tag_internal_name = this.RequestTag?.tag_internal_name;
                    rd.feedback_tag_internal_name = this.RequestFeedbackTag?.tag_internal_name;

                    rd.args_tag_internal_name = this.ArgsTag?.tag_internal_name;
                    rd.event_tag_internal_name = this.EventTag?.tag_internal_name;
                    rd.return_tag_internal_name = this.ReturnTag?.tag_internal_name;

                    rd.cmd_id_dv2plc_tag_internal_name = this.CmdIdTag_Dv2Plc?.tag_internal_name;
                    rd.cmd_index_dv2plc_tag_internal_name = this.CmdIndexTag_Dv2Plc?.tag_internal_name;
                    rd.cmd_value_dv2plc_tag_internal_name = this.CmdValueTag_Dv2Plc?.tag_internal_name;
                    rd.cmd_id_plc2dv_tag_internal_name = this.CmdIdTag_Plc2Dv?.tag_internal_name;
                    rd.cmd_index_plc2dv_tag_internal_name = this.CmdIndexTag_Plc2Dv?.tag_internal_name;
                    rd.cmd_value_plc2dv_tag_internal_name = this.CmdValueTag_Plc2Dv?.tag_internal_name;
                    rd.cmd_return_plc2dv_tag_internal_name = this.CmdReturnTag_Plc2Dv?.tag_internal_name;


                    if (rd.dest_tag_type == 1)
                    {
                        //写单个点，等待Feedback点
                        rd.return_type = Utli.ConvertToEnum<TagDataType>(this.RequestFeedbackTag?.data_type_id.ToString()) == TagDataType.BOOL ? "bool" : "int";
                    }
                    else if(rd.dest_tag_type == 2)
                    {
                        rd.return_type = Utli.ConvertToEnum<TagDataType>(this.ReturnTag?.data_type_id.ToString()) == TagDataType.SHORT_ARRAY ? "int_array" : "long";
                    }
                    else if (rd.dest_tag_type == 3)
                    {
                        rd.return_type = Utli.ConvertToEnum<TagDataType>(this.CmdReturnTag_Plc2Dv?.data_type_id.ToString()) == TagDataType.SHORT ? "int" : "string";
                    }
                }
                if (this.IsSpecialReturnVal && (this.ReturnValueLst == null || this.ReturnValueLst.Count == 0))
                {
                    System.Windows.MessageBox.Show("已选择单独定义返回值，返回值列表不能为空！"); return;
                }
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                };
                DbModels.dv_request_interface tmpDvRequestInterface = new DbModels.dv_request_interface()
                {
                    request_internal_name = this.RequestInternalName,
                    msg_type = this.MsgType,
                    param_separtor = this.ParamSeperator,
                    //param_format = this.EcsComm == ECSCommType.MQ ? "json" : 
                    //Utli.ConvertToEnum<DataView_Configuration.TagDataType>(this.ArgsTag?.data_type_id.ToString()).ToString(),
                    system_id = this.curSystem.system_id,
                    dest_tag_name = JsonConvert.SerializeObject(rd,settings),
                    precondition_id = (int)this.RequestPrecondition,
                    request_param_list = GetParamIdLst(),
                    return_value = this.IsCommReturnVal ? "" : JsonConvert.SerializeObject(this.ReturnValueLst),
                    failed_tips_show = this.IsFailedShowTips,
                    success_tips_show = this.IsSuccessShowTips,
                    special_return_value = this.IsSpecialReturnVal,
                    request_desc = this.RequestDesc,
                };
                if (this.EcsComm == ECSCommType.MQ)
                {
                    tmpDvRequestInterface.param_format = "json";
                }
                else if (this.EcsComm == ECSCommType.OPC)
                {
                    if (this.IsWriteSingleTag)
                    {
                        tmpDvRequestInterface.param_format = Utli.ConvertToEnum<DataView_Configuration.TagDataType>(this.RequestTag?.data_type_id.ToString()).ToString();
                    }
                    else if (this.IsWriteArgsEventsTag)
                    {
                        tmpDvRequestInterface.param_format = Utli.ConvertToEnum<DataView_Configuration.TagDataType>(this.ArgsTag?.data_type_id.ToString()).ToString();
                    }
                    else if (this.IsWriteCmdIdIndexValueTag)
                    {
                        tmpDvRequestInterface.param_format = Utli.ConvertToEnum<DataView_Configuration.TagDataType>(this.CmdValueTag_Dv2Plc?.data_type_id.ToString()).ToString();
                    }
                }
                else if (this.EcsComm == ECSCommType.Rest)
                {
                    tmpDvRequestInterface.param_format = "json";
                }
                try
                {
                    DbHelper.db.BeginTran();
                   
                    //添加新的
                    if (this.IsAddNew)
                    {
                        DbHelper.db.Insertable<DbModels.dv_request_interface>(tmpDvRequestInterface).ExecuteCommand();
                    }
                    //更新已有的
                    else
                    {
                        tmpDvRequestInterface.id = curRequest.RequestId;
                        DbHelper.db.Updateable<DbModels.dv_request_interface>(tmpDvRequestInterface)
                        .Where(x => x.id == tmpDvRequestInterface.id).ExecuteCommand();
                    }
                    DbHelper.db.CommitTran();
                    
                    var win = o as Window;
                    win.DialogResult = true;
                    System.Windows.MessageBox.Show("保存成功！");
                }
                catch (Exception ex)
                {
                    DbHelper.db.RollbackTran();
                    System.Windows.MessageBox.Show("保存失败！"+ex.ToString());
                    LogHelper.Error($"[Confirm RPC Interface]{ex.ToString()}");
                }
               
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("保存失败！" + ex.ToString());
                LogHelper.Error($"[Confirm RPC Interface]{ex.ToString()}");
            }
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
    /// <summary>
    /// 参数校验枚举的实体类转换
    /// </summary>
    internal class ParamValidationRuleModel
    {
        public string RuleName { get; set; }
        public string RuleDesc { get; set; }
        public int RuleID { get; set; }
    }
}
