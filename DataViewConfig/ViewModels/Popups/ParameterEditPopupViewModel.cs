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
    internal class ParameterEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        #region Properties
       
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
                TagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
                    .Select(x => x.tag_internal_name).ToList());
                //ControlInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_control_conf>()
                //    .Select(x => x.dv_control_internal_name).ToList());
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
                this.SelectedControl = ControlLst?.Where(x=>x.dv_control_internal_name== paramModel.ControlInternalName).FirstOrDefault();
                this.SelectedControlComboxDataType = paramModel.ControlComboxIndexOrTextType;
                this.TagInternalName = paramModel.TagInternalName;
                this.SelectedTag = TagLst.Where(x => x.tag_internal_name == this.TagInternalName).FirstOrDefault();
                this.TagJsonPath = paramModel.TagValueJsonPath;
                this.ArrayParamIdLst = paramModel.ArrayParamIdLst;
                //
                this.CurParamTargetType = paramModel.TargetValType;
                this.ParamDesc=paramModel.ParamDesc;
             
                if (paramModel.ReturnValueLst != null)
                {
                    this.RequestReturnValLst = new ObservableCollection<RequestSpecialReturnValueModel>(paramModel.ReturnValueLst);
                }
                else
                {
                    this.RequestReturnValLst = new ObservableCollection<RequestSpecialReturnValueModel>();
                }


            }
        }
        
        public bool IsAddNew { get; set; }
        //public bool IsReturnIntArray { get; set; }
        //public bool IsReturnLong { get; set; }
       
        public bool IsCheckParamValidation { get; set; }
        //public bool IsSelectParam { get; set; }
        //public bool IsAddNewParam { get; set; }
        //public bool IsEditParam { get; set; }
        
        public ObservableCollection<ParamValidationRuleModel> ParamValidationRuleLst { get; set; }
       
        public ObservableCollection<RequestParamContentModel.ValidationRule> SelectedParamValidationRuleLst { get; set; }
        public ObservableCollection<dv_request_param> DbParamLst { get; set; }
        public ObservableCollection<dv_control_conf> DbControlLst { get; set; }
        public ObservableCollection<dv_tag> DbTagLst { get; set; }
        public ObservableCollection<dv_tips> DvTipsLst { get; set; }

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
        
        #endregion 
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
        public ParameterEditPopupViewModel(ParamModel paramModel)
        {
            DbParamLst = new ObservableCollection<dv_request_param>(DbHelper.db.Queryable<DbModels.dv_request_param>().ToList());
            DbTagLst = new ObservableCollection<DbModels.dv_tag>(DbHelper.db.Queryable<DbModels.dv_tag>().ToList());
            DvTipsLst = new ObservableCollection<dv_tips>(DbHelper.db.Queryable<DbModels.dv_tips>().ToList());
            DbControlLst = new ObservableCollection<dv_control_conf>(DbHelper.db.Queryable<DbModels.dv_control_conf>().ToList());
            if (paramModel != null)
            {
                this.ParamID = paramModel.ParamID;
                this.ParameterInternalName = paramModel.ParamName;
                this.ParamDesc = paramModel.ParamDesc;
                this.ParamExpressionStr = paramModel.ExpressionStr;
                this.CurParamrSource = paramModel.ParamSource;
                switch (this.CurParamrSource)
                {
                    case RequestParamSource.CONSTANT:
                        this.ConstValue = paramModel.ConstValue; ;
                        break;
                    case RequestParamSource.CONTROL:
                        this.SelectedControl = DbControlLst.Where(x => x.dv_control_internal_name == paramModel.ControlInternalName).FirstOrDefault();
                        this.SelectedControlComboxDataType = paramModel.ControlComboxIndexOrTextType;
                        if (this.SelectedControlComboxDataType == 0)
                        {
                            SelectedControlComboxDataType = 1;
                        }
                        break;
                    case RequestParamSource.JSON_TAG:
                        this.SelectedTag = DbTagLst.Where(x => x.tag_internal_name == paramModel.TagInternalName).FirstOrDefault();
                        this.TagJsonPath = paramModel.TagValueJsonPath;
                        break;
                    case RequestParamSource.NORMAL_TAG:
                        this.SelectedTag = DbTagLst.Where(x => x.tag_internal_name == paramModel.TagInternalName).FirstOrDefault();
                        break;
                    case RequestParamSource.MACRO:
                        this.CurMacroName = paramModel.MacroName;
                        break;

                }
                this.ParamJsonVariableName = paramModel.JsonVariableName;
                this.CurParamTargetType = paramModel.TargetValType;
            }
            else
            {
                IsAddNew = true;
            }
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
            
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            SelectParamCommand = new Command(SelectParam);
           

            ConfirmAddParameterCommand = new Command(ConfirmAddParameter);
            ConfirmSelectParameterCommand = new Command(ConfirmSelectParameter);
            CancelAddParameterCommand = new Command(CancelAddParameter);
           
            EditRpcParamCommand = new Command(EditRpcParam);
            RemoveSelectedRpcParamCommand = new Command(RemoveSelectedParam);

            //返回值
        

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
                   
                    

                    ArrayItemsParamStr = string.Empty;
                   
                    
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
               
                this.SelectedParam = null;
                

                var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
                
               
                //数组选择子参数
                ArrayItemsParamStr = string.Empty;
               
              
                return;
            }
            else
            {
                MessageBox.Show("删除失败！");
               
            }
        }
        public void UpdateParamItems(string str)
        {
        }
       
       
        private bool IsEditExistReturnVal = false;
      
        
      
       
        
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
        
       
       
        
        /// <summary>
        /// 编辑参数
        /// </summary>
        /// <param name="o"></param>
        private void EditRpcParam(object o)
        {
            try
            {
                var paramModel = o as ParamModel;
               
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
               
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("打开编辑页面失败！" + ex.ToString());
                LogHelper.Error($"[EditRpcParam]{ex.ToString()}");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">参数列表</param>
        private void SelectParam(object o)
        {
            //short[] paramIdArray = null;
            //if (o!=null&&!string.IsNullOrEmpty(o.ToString()))
            //{
            //   paramIdArray = Array.ConvertAll<string, short>(o.ToString().Split(','), s => Convert.ToInt16(s));
            //}
            //Pages.Popups.ParamSelectedPopup paramSelectPopup = new Pages.Popups.ParamSelectedPopup(paramIdArray);
            //var bRet=paramSelectPopup.ShowDialog();
            //if (bRet.Value)
            //{
            //    this.ParamIdLst =
            //   String.Join(",", (paramSelectPopup.DataContext as ViewModels.ParamSelectedPopupViewModel).paramIdLst.ToArray());
            //}
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
                    rp.value_expression = this.ParamExpressionStr;
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
            
            var addedParamId = DbHelper.db.Insertable<DbModels.dv_request_param>(c).ExecuteReturnIdentity();
            if (addedParamId > 0)
            {
                MessageBox.Show("添加成功！");
                c.param_id = addedParamId;
                //AllParamLst = new ObservableCollection<ParamModel>();
               
                //var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
                //foreach (var item in paramLst)
                //{
                //    AllParamLst.Add(dvRequestSystem2ParamModel(item));
                //}
                ArrayItemsParamStr = string.Empty;
               
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
          
        }
        private void CancelAddParameter(object o)
        {
            
        }
        //参数编辑 确认
        private void Confirm(object o)
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
              
                DbModels.dv_request_param c = new DbModels.dv_request_param()
                {
                    //param_id = this.ParamID,
                    param_name = this.ParameterInternalName,
                    param_json_variable_name = string.IsNullOrEmpty(this.ParamJsonVariableName) ? "default" : this.ParamJsonVariableName,
                    param_source = (int)this.CurParamrSource,
                    param_content = JsonConvert.SerializeObject(rp),
                    value_type = this.CurParamTargetType.ToString(),
                    param_desc = this.ParamDesc,
                };
                if (IsAddNew)
                {
                    var affectedRow = DbHelper.db.Insertable<DbModels.dv_request_param>(c).ExecuteCommand();
                    if (affectedRow > 0)
                    {
                        MessageBox.Show("保存成功！");
                        var win = o as Window;
                        win.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("保存失败！");
                    }
                }
                else
                {
                    c.param_id = this.ParamID;
                    var affectedRow = DbHelper.db.Updateable<DbModels.dv_request_param>(c).Where(x => x.param_id == c.param_id).ExecuteCommand();
                    if (affectedRow > 0)
                    {
                        MessageBox.Show("保存成功！");
                        var win = o as Window;
                        win.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("保存失败！");
                    }
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("保存失败！" + ex.ToString());
                LogHelper.Error($"[Confirm Parameter]{ex.ToString()}");
            }
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
  
}
