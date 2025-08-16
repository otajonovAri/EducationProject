namespace EducationApp.Core;

public enum Permission
{
    #region Admin Permissions

    AdminPermission,

    #endregion

    #region Group Permissions

    GetAllGroupPermission,
    GetByIdGroupPermission,
    CreateGroupPermission,
    UpdateGroupPermission,
    DeleteGroupPermission,

    #endregion

    #region Attendance Permissions

    GetAllAttendancePermission,
    GetByIdAttendancePermission,
    CreateAttendancePermission,
    UpdateAttendancePermission,
    DeleteAttendancePermission,

    #endregion

    #region File Permissions

    UploadFilePermission,
    DownloadFilePermission,
    DeleteFilePermission,

    #endregion

    #region GroupSubject Permissions

    GetAllGroupSubjectPermission,
    GetByIdGroupSubjectPermission,
    CreateGroupSubjectPermission,
    UpdateGroupSubjectPermission,
    DeleteGroupSubjectPermission,

    #endregion

    #region Payment Permissions

    GetAllPaymentPermission,
    GetByIdPaymentPermission,
    CreatePaymentPermission,
    UpdatePaymentPermission,
    DeletePaymentPermission,

    #endregion

    #region PaymentDocument Permissions

    GetAllPaymentDocumentPermission,
    GetByIdPaymentDocumentPermission,
    CreatePaymentDocumentPermission,
    UpdatePaymentDocumentPermission,
    DeletePaymentDocumentPermission,

    #endregion

    #region Permission Permissions

    CreatePermissionPermission,
    GetAllPermissionPermission,

    #endregion

    #region Role Permissions

    CreateRolePermission,
    AssignPermissionToRolePermission,

    #endregion

    #region Room Permissions

    GetAllRoomPermission,
    GetByIdRoomPermission,
    CreateRoomPermission,
    UpdateRoomPermission,
    DeleteRoomPermission,

    #endregion

    #region Staff Permissions

    GetAllStaffPermission,
    GetByIdStaffPermission,
    CreateStaffPermission,
    UpdateStaffPermission,
    DeleteStaffPermission,

    #endregion

    #region StaffSubject Permissions

    GetAllStaffSubjectPermission,
    GetByIdStaffSubjectPermission,
    CreateStaffSubjectPermission,
    UpdateStaffSubjectPermission,
    DeleteStaffSubjectPermission,

    #endregion

    #region Student Permissions

    GetAllStudentPermission,
    GetByIdStudentPermission,
    CreateStudentPermission,
    UpdateStudentPermission,
    DeleteStudentPermission,

    #endregion

    #region Subject Permissions

    GetAllSubjectPermission,
    GetByIdSubjectPermission,
    CreateSubjectPermission,
    UpdateSubjectPermission,
    DeleteSubjectPermission,

    #endregion

    #region User Permissions

    GetAllUserPermission,
    GetByIdUserPermission,
    CreateUserPermission,
    UpdateUserPermission,
    DeleteUserPermission,

    #endregion

    #region UserRole Permissions

    AssignRolesToUserPermission,

    #endregion
}
