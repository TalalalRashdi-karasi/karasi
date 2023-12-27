// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {



    $(document).ready(function () {
        $('#categoryFilter').change(function () {
            var selectedCategory = $(this).val();

            if (selectedCategory === "") {
                $('#itemTable tbody tr').show();
            } else {
                $('#itemTable tbody tr').hide();
                $('#itemTable tbody tr[data-category="' + selectedCategory + '"]').show();
            }
        });
    });


    $('.loadPartialViewButton').on('click', function () {
        var eventId = $(this).data('eventid');
        $.ajax({
            type: 'GET',
            url: '/Admin/GetEventById',
            data: { EvID: eventId },
            success: function (result) {
                $('#partialViewContainer').html(result);
                // Update the container with the received partial view
                // Assuming your container has an ID, replace '#partialViewContainer' with your actual container ID
                
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
});



$(document).ready(function () {
    $('#EventListForm').submit(function (event) {
        // Prevent the default form submission
        event.preventDefault();

        // Display an alert to confirm the form submission
        if (confirm('هل انت متأكد من خذف الفعالية؟')) {
            // If the user clicks OK in the confirmation alert, proceed with Ajax submission
            $.ajax({
                type: 'POST', // or 'GET' depending on your form submission method
                url: '/Admin/DeleteEventById', // Replace with the actual URL where your form should be submitted
                data: $('#EventListForm').serialize(), // Serialize the form data
                success: function (response) {
              
                    // Handle the success response here
                    console.log('Form submitted successfully!', response);
                    location.reload(true);
                },
                error: function (error) {
                    // Handle the error response here
                    console.error('Error submitting form:', error);
                }
            });
        } else {
            // If the user clicks Cancel in the confirmation alert, do nothing
            console.log('Form submission canceled.');
        }
    });
});

$(document).ready(function () {
    $("#SignUpForm").submit(function (event) {
        // Prevent default form submission
        event.preventDefault();

        
        // Perform client-side validation
        if (validateForm()) {
            var formData = $(this).serialize(); // Serialize the form data
            $.ajax({
                url: "/Account/register", // Replace with your controller and action URL
                type: "POST",
                data: formData,
                success: function (result) {
                    // Optionally, you can reset the form
                    $("#SignUpForm")[0].reset();
                    // Handle the success response
                    console.log(result);
                    window.location = "/Home/Index";
                },
                error: function (error) {
                    // Handle the error response
                    console.log(error);
                }
            });
        }
    });

    function validateForm() {
        // Get form input values
        var usernameField = $("#Firstname");
        var passwordField = $("#Password");
        var EmailField = $("#Email");
        var UserTypeField = $("#UserType");
        var SexField = $("#Sex");


                // Get values from the elements
                var username = usernameField.val();
                var password = passwordField.val();
                var Email = EmailField.val();
                var Sex = SexField.val();
                var UserType = UserTypeField.val();
        
        
        
        // Perform validation checks
        if (username.trim() === "" || password.trim() === "" || Email.trim() === "" || Sex.trim() === ""  ) {

            usernameField.css("border", "1px solid red");
            passwordField.css("border","1px solid red");
            EmailField.css("border","1px solid red");
            UserTypeField.css("border","1px solid red");
            SexField.css("border","1px solid red");
            return false;
        }




        

        // Additional validation logic can be added here

        // If all validation checks pass, return true
        return true;
    }


       // Remove the red border when the user starts typing in an input field
       $("#Firstname, #Password, #Email, #UserType, #Sex").on("input", function () {
        $(this).css("border", "");
    });
});




$(document).ready(function () {

    


    var data = [
        { id:"1", fname:"Tiger", lname:"Noxx", team:'Team 1', address:'Ryecroft Field',   tel:'0494645879'},
        { id:"2", fname:"Garrett", lname:"Pellens", team:'Team 2', address:'Kiln Circus',      tel:'0493658746' },
      ];

        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            var count = parseInt($input.val()) - 1;
            count = count < 1 ? 1 : count;
            $input.val(count);
            $input.change();
            return false;
        });
        $('.plus').click(function () {
            var $input = $(this).parent().find('input');
            $input.val(parseInt($input.val()) + 1);
            $input.change();
            return false;
        });



    


function prev(){
    document.getElementById('slider-container').scrollLeft -= 270;
}

function next()
{
    document.getElementById('slider-container').scrollLeft += 270;
}


$(".slide img").on("click" , function(){
$(this).toggleClass('zoomed');
$(".overlay").toggleClass('active');
})

});



