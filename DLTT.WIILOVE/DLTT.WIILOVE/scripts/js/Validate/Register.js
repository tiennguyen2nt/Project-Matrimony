$(document).ready(function () {
    $('#formValidator').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Password: {
                message: 'The Password is not valid',
                validators: {
                    notEmpty: {
                        message: 'Mật khẩu không được bỏ trống'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'Mật khẩu phải có từ 6 đến 30 ký tự.'
                    }

                }
            },
            FirstName: {
                validators: {
                    notEmpty: {
                        message: 'Vui lòng nhập Tên'
                    },
                    regexp: {
                        regexp: /^([A-Za-z]|[^\x00-\x7F])+$/,
                        message: 'Tên chỉ bao gồm chữ.'
                    }
                }
            },
            LastName: {
                validators: {
                    notEmpty: {
                        message: 'Vui lòng nhập Họ'
                    },
                    regexp: {
                        regexp: /^([A-Za-z]|[^\x00-\x7F])+$/,
                        message: 'Họ chỉ bao gồm chữ.'
                    }
                }
            }, RetypePassword: {
                validators: {
                    notEmpty: {
                        message: 'Vui lòng nhập lại mật khẩu'
                    },
                    identical: {
                        field: 'Password',
                        message: 'Mật khẩu không trùng khớp'
                    }
                }
            },
            EmailOrPhone: {
                validators: {
                    notEmpty: {
                        message: 'Vui lòng nhập địa chỉ Email hoặc số điện thoại'
                    },
                    regexp: {
                        regexp: /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)|(1?(-?\d{3})-?)?(\d{3})(-?\d{7})$/,
                        message: 'Không đúng định dạng email hoặc số điện thoại.'
                    }
                    //, remote: {
                    //    url: '../Account/IsExistEmailorPhone',
                    //    type: "post",
                    //    message: 'Email hoặc số điện thoại đã tồn tại.'
                    //}
                }
            },
            checkTerms: {
                validators: {
                    required: true
                }
            }

        }
    });


});