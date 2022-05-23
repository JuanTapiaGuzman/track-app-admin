"use strict";

var myDropzone = null;

// Class definition
var KTAppEcommerceSaveProduct = function () {

    // Private functions

    // Init form repeater --- more info: https://github.com/DubFriend/jquery.repeater
    const initFormRepeater = () => {
        $('#kt_ecommerce_add_product_options').repeater({
            initEmpty: false,

            defaultValues: {
                'text-input': 'foo'
            },

            show: function () {
                $(this).slideDown();

                // Init select2 on new repeated items
                initConditionsSelect2();
            },

            hide: function (deleteElement) {
                $(this).slideUp(deleteElement);
            }
        });
    }

    // Init condition select2
    const initConditionsSelect2 = () => {
        // Tnit new repeating condition types
        const allConditionTypes = document.querySelectorAll('[data-kt-ecommerce-catalog-add-product="product_option"]');
        allConditionTypes.forEach(type => {
            if ($(type).hasClass("select2-hidden-accessible")) {
                return;
            } else {
                $(type).select2({
                    minimumResultsForSearch: -1
                });
            }
        });
    }


    // Init DropzoneJS --- more info:
    const initDropzone = () => {
        myDropzone = new Dropzone("#kt_ecommerce_add_product_media", {
            url: "~/../../../UploadFiles/UploadSercommInventoryEntryFile", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            renameFile: function (file) {
                let newName = document.getElementsByName('ReservationNumber')[0].value + ".pdf";
                return newName;
            },
            maxFiles: 1,
            maxFilesize: 10, // MB
            createImageThumbnails: true,
            uploadMultiple: false,
            autoProcessQueue: false,
            acceptedFiles: 'application/pdf',
            withCredentials: false,
            addRemoveLinks: true,
            accept: function (file, done) {
                done();
            }
        });
    }

    // Condition type handler
    const handleConditions = () => {
        const allConditions = document.querySelectorAll('[name="method"][type="radio"]');
        const conditionMatch = document.querySelector('[data-kt-ecommerce-catalog-add-category="auto-options"]');
        allConditions.forEach(radio => {
            radio.addEventListener('change', e => {
                if (e.target.value === '1') {
                    conditionMatch.classList.remove('d-none');
                } else {
                    conditionMatch.classList.add('d-none');
                }
            });
        })
    }

    // Submit form handler
    const handleSubmit = () => {
        // Define variables
        let validator;

        // Get elements
        const form = document.getElementById('kt_ecommerce_add_product_form');
        const submitButton = document.getElementById('kt_ecommerce_add_product_submit');

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'ReservationNumber': {
                        validators: {
                            notEmpty: {
                                message: 'El n&uacute;mero de reserva es un campo obligatorio'
                            }
                        }
                    },
                    'EmployeeId': {
                        validators: {
                            notEmpty: {
                                message: 'La tarjeta del empleado es un campo obligatorio'
                            }
                        }
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Revalidate Select2 input. For more info, plase visit the official plugin site: https://select2.org/
        $(form.querySelector('[name="Item"]')).on('change', function () {
            // Revalidate the field when an option is chosen
            validator.revalidateField('Item');
        });

        // Handle submit button
        submitButton.addEventListener('click', e => {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable submit button whilst loading
                        submitButton.disabled = true;
                        submitButton.removeAttribute('data-kt-indicator');
                        myDropzone.processQueue();

                        let request = new XMLHttpRequest();
                        request.open("POST", "~/../../../SercommInventory/SaveSercommInventoryEntry");

                        var formData = new FormData();
                        formData.append('ReservationNumber', document.getElementsByName('ReservationNumber')[0].value);
                        formData.append('EmployeeId', document.getElementsByName('EmployeeId')[0].value);

                        var items = [];

                        for (var i = 0; document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][Item]') && document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][ItemQuantity]'); i++) {
                            if (document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][Item]')[0] === undefined || document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][ItemQuantity]')[0] === undefined) {
                                break;
                            } else {
                                let item = new Object();
                                item.Id = parseInt(document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][Item]')[0].value);
                                item.Quantity = parseInt(document.getElementsByName('kt_ecommerce_add_product_options[' + i + '][ItemQuantity]')[0].value);

                                items.push(item);
                            }
                        }

                        formData.append('Items', JSON.stringify(items));

                        request.send(formData);
                        request.onreadystatechange = function () {
                            if (request.readyState == 4 && request.status == 200) {
                                Swal.fire({
                                    text: "El formulario se ha guardado exitosamente!",
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, entendido!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                }).then(function (result) {
                                    if (result.isConfirmed) {
                                        // Enable submit button after loading
                                        submitButton.disabled = false;

                                        // Redirect to customers list page
                                        window.location = form.getAttribute("data-kt-redirect");
                                    }
                                });
                            } else {
                                Swal.fire({
                                    html: "Lo siento, parece que se ha detectado un error al guardar, por favor intente otra vez. <br/><br/><strong>Verifique que esten llenos todos los campos en el formulario</strong>",
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, entendido!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                });
                                submitButton.disabled = false;
                            }
                        }
                    } else {
                        Swal.fire({
                            html: "Lo siento, parece que se ha detectado un error al guardar, por favor intente otra vez. <br/><br/><strong>Verifique que esten llenos todos los campos en el formulario</strong>",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, entendido!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                        submitButton.disabled = false;
                    }
                });
            }
        })
    }

    // Public methods
    return {
        init: function () {
            // Init forms
            initFormRepeater();
            initDropzone();
            initConditionsSelect2();

            // Handle forms
            handleConditions();
            handleSubmit();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTAppEcommerceSaveProduct.init();
});
