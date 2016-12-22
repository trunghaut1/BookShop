// Write your Javascript code.
function AddToCart(id, quantity, related)
{
    $.ajax({
        type: 'get',
        url: '/cart/checkbook',
        data: { id : id},
        success: function (result) {
            if(result)
            {
                $.ajax({
                    type: 'post',
                    url: '/cart/addtocart',
                    data: {
                        id: id,
                        quantity: quantity
                    },
                    success: function (response) {
                        if (response) {
                            var url = window.location.href.toLowerCase().indexOf("cart");
                            if (url > 0) {
                                location.reload();
                            }
                            else {
                                if (related)
                                {
                                    $("#relatedProModal").modal('hide');
                                    $.ajax({
                                        type: 'get',
                                        url: '/home/relatedpartial',
                                        data: {
                                            id: id
                                        },
                                        success: function (relhtml) {
                                            if (relhtml != null)
                                            {
                                                $("#relModalBody").html(relhtml);
                                                $("#relatedProModal").modal('show');
                                            }
                                        }
                                    });
                                }
                                $.ajax({
                                    type: 'get',
                                    url: '/cart/refreshcart',
                                    success: function (html) {

                                        var container = $("#cartSummary");
                                        container.html(html)
                                        $("#cartNumber").text($("#cartTotalItem").val());
                                    }
                                });
                            }
                        }
                        else
                        {
                            $.notify({
                                // options
                                message: 'Lượng hàng trong kho của sản phẩm hiện không đủ'
                            }, {
                                // settings
                                type: 'danger',
                                placement: {
                                    from: "bottom",
                                    align: "center"
                                },
                                animate: {
                                    enter: 'animated fadeInUp',
                                    exit: 'animated fadeOutDown'
                                },
                                delay: 2000,
                                mouse_over: "pause"
                            });
                        }
                    }
                });
            }
            else
            {
                $.notify({
                    // options
                    message: 'Sản phẩm không khả dụng ở thời điểm hiện tại, có thể đã hết thời gian bày bán'
                }, {
                    // settings
                    type: 'warning',
                    placement: {
                        from: "bottom",
                        align: "center"
                    },
                    animate: {
                        enter: 'animated fadeInUp',
                        exit: 'animated fadeOutDown'
                    },
                    delay: 2000,
                    mouse_over: "pause"
                });
            }
        }
    });
}
function RemoveFromCart(id) {
    $.ajax({
        type: 'post',
        url: '/cart/removeFromCart',
        data: {
            id: id
        },
        success: function (response) {
            if (response) {
                var url = window.location.href.toLowerCase().indexOf("cart");
                if (url > 0)
                {
                    location.reload();
                }
                else
                {
                    $.ajax({
                        type: 'get',
                        url: '/cart/refreshcart',
                        success: function (html) {

                            var container = $("#cartSummary");
                            container.html(html)
                            $("#cartNumber").text($("#cartTotalItem").val());
                        }
                    });
                }
            }
        }
    });
}
function DownCart(id)
{
    $.ajax({
        type: 'post',
        url: '/cart/DownCart',
        data: {
            id: id
        },
        success: function (response) {
            if (response) {
                location.reload();
            }
        }
    });
}
function CartChange(id, quantity) {
    if (quantity < 1) return;
    $.ajax({
        type: 'get',
        url: '/cart/cartchange',
        data: {
            id: id,
            quantity: quantity
            },
                success: function (response) {
                    if (response) {
                        location.reload();
                    }
                    else
                    {
                        $.notify({
                            // options
                            message: 'Lượng hàng trong kho của sản phẩm hiện không đủ'
                        }, {
                            // settings
                            type: 'danger',
                            placement: {
                                from: "bottom",
                                align: "center"
                            },
                            animate: {
                                enter: 'animated fadeInUp',
                                exit: 'animated fadeOutDown'
                            },
                            delay: 2000,
                            mouse_over: "pause"
                        });
                    }
                }
    });
}

function ShowOrder(order)
{
    $("#order-id").html(order.id);
    $("#order-date").html(order.date);
    $("#order-address").html(order.address);
    if(order.shipped)
    {
        $("#order-shipped").html("Đã giao");
    }
    else
    {
        $("#order-shipped").html("Chưa giao");
    }
}
function ShowOrderDetail(id)
{
    $.ajax({
        type: 'get',
        url: '/order/showorderdetail',
        data: {
            id: id
        },
        success: function (response) {
            $("#modalBody").html(response);
        }
    });
}
