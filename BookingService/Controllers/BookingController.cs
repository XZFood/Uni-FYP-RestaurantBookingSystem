﻿using DatabaseContext.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookingService.Controllers
{
    [RoutePrefix("Booking")]
    public class BookingController : ApiController
    {
        private BookingSystemDb _db;
        private readonly bool isTesting = false;

        /// <summary>
        /// Default constructor that sets the database to be an instance of BookingSystemDb
        /// </summary>
        public BookingController()
        {
            _db = new BookingSystemDb();
        }

        /// <summary>
        /// Constructor used when unit testing to pass a mock of the BookingSystemDb
        /// </summary>
        /// <param name="db"></param>
        public BookingController(BookingSystemDb db)
        {
            isTesting = true;
            this._db = db;
        }

        /// <summary>
        /// Gets all bookings from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Get/{id:int?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id = -1)
        {
            if (id != -1)
            {
                Booking res = await _db.Bookings.Where(b => b.Id == id && !b.Deleted).FirstOrDefaultAsync();

                if (res == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Booking Found With ID");

                LibBookingService.Dtos.Booking booking = CreateBoookingFromDbBooking(res);

                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            else
            {
                IEnumerable<Booking> res = await _db.Bookings.Where(b => b.Deleted != true).ToListAsync();

                IEnumerable<LibBookingService.Dtos.Booking> bookings = res.Select(b => CreateBoookingFromDbBooking(b));

                return bookings.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, bookings) :
                    Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Bookings");
            }
        }

        /// <summary>
        /// Gets all bookings by customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetByCustomer/{id:int?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByCustomer(int id = -1)
        {
            if (id != -1)
            {
                IEnumerable<Booking> res = await _db.Bookings.Where(b => !b.Deleted && b.Customer_id == id).ToListAsync();

                if (!res.Any())
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Bookings Found With ID");

                return Request.CreateResponse(HttpStatusCode.OK, res.Select(b => CreateBoookingFromDbBooking(b)));
            }

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Invalid Customer Id");
        }

        /// <summary>
        /// Gets a list of available tables using a booking model
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        [Route("GetAvailableTable")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetAvailableTable(LibBookingService.Dtos.Booking booking)
        {
            if (booking == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid booking model");

            IEnumerable<Booking> res = await _db.Bookings.Where(b => !b.Deleted && !b.Cancelled).ToListAsync();
            res = res.Where(b => b.Restaurant_id == booking.RestaurantId);
            res = res.Where(b => b.BookingDate.Date == booking.BookingDate.Date);

            if (!res.Any())
            {
                IEnumerable<Table> restaurantTables = _db.Tables.Where(t => t.Restaurant_id == booking.RestaurantId).OrderBy(t => t.NoSeats);
                restaurantTables = restaurantTables.Where(t => !t.Deleted && t.Active);
                restaurantTables = restaurantTables.Where(t => t.NoSeats >= booking.NoCustomers);

                if (restaurantTables.Any())
                {
                    Table restaurantTable = restaurantTables.FirstOrDefault();

                    return Request.CreateResponse(HttpStatusCode.OK, new LibBookingService.Dtos.Table
                    {
                        Id = restaurantTable.Id,
                        RestaurantId = restaurantTable.Restaurant_id,
                        AdditionalNotes = restaurantTable.AdditionalNotes,
                        NoSeats = restaurantTable.NoSeats,
                        TableNo = restaurantTable.TableNo
                    });
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No tables available");
                }
            }

            if (booking.StartTime != null && booking.EndTime != null)
            {
                res = res.Where(b => 
                    (TimeSpan.Compare(booking.StartTime, b.StartTime) == 1 && TimeSpan.Compare(booking.StartTime, b.EndTime) == -1) ||
                    (TimeSpan.Compare(booking.EndTime, b.StartTime) == 1 && TimeSpan.Compare(booking.EndTime, b.EndTime) == -1) ||
                    (booking.StartTime == b.StartTime && booking.EndTime == b.EndTime)
                );

                IEnumerable<Table> resTables = res.SelectMany(b => b.TableBookings.Select(bb => bb.Table));

                IEnumerable<Table> restaurantTables = _db.Tables.Where(t => t.Restaurant_id == booking.RestaurantId).OrderBy(t => t.NoSeats);
                restaurantTables = restaurantTables.Where(t => !t.Deleted && t.Active);
                restaurantTables = restaurantTables.Where(t => t.NoSeats >= booking.NoCustomers);

                List<Table> result = new List<Table>();

                foreach (var t in restaurantTables)
                {
                    if (resTables.Where(tt => tt.Id == t.Id).FirstOrDefault() == null)
                        result.Add(t);
                }

                if (result.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, new LibBookingService.Dtos.Table
                    {
                        Id = result.First().Id,
                        RestaurantId = result.First().Restaurant_id,
                        AdditionalNotes = result.First().AdditionalNotes,
                        NoSeats = result.First().NoSeats,
                        TableNo = result.First().TableNo
                    });
            }

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No tables available");
        }

        /// <summary>
        /// Posts a booking Dto to the database.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(LibBookingService.Dtos.Booking booking)
        {
            try
            {
                Booking newBoooking = _db.Bookings.Add(new Booking
                {
                    Customer_id = booking.CustomerId,
                    Restaurant_id = booking.RestaurantId,
                    BookingMadeDate = booking.BookingMadeDate,
                    BookingMadeTime = booking.BookingMadeTime,
                    BookingDate = booking.BookingDate,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    PaymentTotal = booking.PaymentTotal,
                    PaymentMadeDate = booking.PaymentMadeDate,
                    NoCustomers = booking.NoCustomers,
                    Comments = booking.Comments,
                    Cancelled = booking.Cancelled
                });
                await _db.SaveChangesAsync();

                if (booking.Tables != null)
                {
                    foreach (LibBookingService.Dtos.Table t in booking.Tables)
                    {
                        _db.TableBookings.Add(new TableBooking
                        {
                            Table_id = t.Id,
                            Booking_id = newBoooking.Id
                        });
                        await _db.SaveChangesAsync();
                    }
                }

                if (booking.Payments != null)
                {
                    foreach (LibBookingService.Dtos.Payment p in booking.Payments)
                    {
                        _db.Payments.Add(new Payment
                        {
                            PaymentMethod_id = p.PaymentMethod.Id,
                            Customer_id = p.CustomerId,
                            Booking_id = p.BookingId,
                            Amount = p.Amount,
                            Comments = p.Comments
                        });
                        await _db.SaveChangesAsync();
                    }
                }

                if (booking.MenuItems != null)
                {
                    foreach (LibBookingService.Dtos.BookingMenuItem m in booking.MenuItems)
                    {
                        _db.BookingMenuItems.Add(new BookingMenuItem
                        {
                            Booking_id = newBoooking.Id,
                            MenuItem_id = m.MenuItemId,
                            Quantity = m.Quantity
                        });
                        await _db.SaveChangesAsync();
                    }
                }

                if (!isTesting)
                {
                    _db.Entry(newBoooking).Reload();
                }

                return Request.CreateResponse(HttpStatusCode.OK, CreateBoookingFromDbBooking(newBoooking));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
        }

        /// <summary>
        /// Removes a booking.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete/{id:int?}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                Booking booking = await _db.Bookings.Where(b => b.Id == id).FirstOrDefaultAsync();
                booking.Deleted = true;

                _db.SetModified(booking);
                await _db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
        }

        /// <summary>
        /// Cancels a booking.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Cancel/{id:int?}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Cancel(int id)
        {
            try
            {
                Booking booking = await _db.Bookings.Where(b => b.Id == id).FirstOrDefaultAsync();
                booking.Cancelled = true;

                _db.SetModified(booking);
                await _db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
        }

        /// <summary>
        /// Puts a booking to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        [Route("Update/{id:int?}")]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, LibBookingService.Dtos.Booking booking)
        {
            try
            {
                Booking b = await _db.Bookings.Where(bb => bb.Id == id).FirstOrDefaultAsync();

                b.Customer_id = booking.CustomerId;
                b.Restaurant_id = booking.RestaurantId;
                b.BookingMadeDate = booking.BookingMadeDate;
                b.BookingMadeTime = booking.BookingMadeTime;
                b.BookingDate = booking.BookingDate;
                b.StartTime = booking.StartTime;
                b.EndTime = booking.EndTime;
                b.PaymentTotal = booking.PaymentTotal;
                b.PaymentMadeDate = booking.PaymentMadeDate;
                b.NoCustomers = booking.NoCustomers;
                b.Comments = booking.Comments;
                b.Cancelled = booking.Cancelled;

                _db.SetModified(b);
                await _db.SaveChangesAsync();

                LibBookingService.Dtos.Booking res = CreateBoookingFromDbBooking(b);

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
        }

        /// <summary>
        /// Returns a booking model using the database booking parameter.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private LibBookingService.Dtos.Booking CreateBoookingFromDbBooking(Booking b)
        {
            return new LibBookingService.Dtos.Booking
            {
                Id = b.Id,
                CustomerId = b.Customer_id,
                RestaurantId = b.Restaurant_id,
                Restaurant = b.Restaurant != null ? new LibBookingService.Dtos.Restaurant
                {
                    Id = b.Restaurant.Id,
                    CompanyId = b.Restaurant.Company_id,
                    Name = b.Restaurant.Name,
                    PhoneNo = b.Restaurant.PhoneNo,
                    AddressStreet = b.Restaurant.AddressStreet,
                    AddressTown = b.Restaurant.AddressTown,
                    AddressCounty = b.Restaurant.AddressCounty,
                    AddressPostalCode = b.Restaurant.AddressPostalCode,
                    Tables = b.Restaurant.Tables.Select(t => new LibBookingService.Dtos.Table { Id = t.Id })
                } : null,
                BookingMadeDate = b.BookingMadeDate,
                BookingMadeTime = b.BookingMadeTime,
                BookingDate = b.BookingDate,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                PaymentTotal = b.PaymentTotal,
                PaymentMadeDate = b.PaymentMadeDate,
                NoCustomers = b.NoCustomers,
                Comments = b.Comments,
                Cancelled = b.Cancelled,
                Tables = GetTablesForBooking(b),
                Payments = GetPaymentsForBooking(b),
                MenuItems = GetBookingMenuItemsForBooking(b)
            };
        }

        /// <summary>
        /// Returns a list of tables from the booking parameter.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        private IEnumerable<LibBookingService.Dtos.Table> GetTablesForBooking(Booking booking)
        {
            IEnumerable<TableBooking> tb = booking.TableBookings.Where(b => !b.Deleted);
            IEnumerable<Table> tbTables = tb.Select(b => _db.Tables.Where( bb => bb.Id == b.Table_id).FirstOrDefault());
            IEnumerable<Table> tbTables1 = tbTables.Where(b => !b.Deleted);

            //IEnumerable<Table> bookingTables = booking.TableBookings.Where(b => !b.Deleted).Select(b => b.Table).Where(b => !b.Deleted);
            if (tbTables1.Any())
                return tbTables1.Select(t => new LibBookingService.Dtos.Table
                {
                    Id = t.Id,
                    RestaurantId = t.Restaurant_id,
                    NoSeats = t.NoSeats,
                    AdditionalNotes = t.AdditionalNotes ?? "",
                    Active = t.Active
                });
            return Enumerable.Empty<LibBookingService.Dtos.Table>();
        }

        /// <summary>
        /// Returns a list of payments from the booking parameter.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        private IEnumerable<LibBookingService.Dtos.Payment> GetPaymentsForBooking(Booking booking)
        {
            IEnumerable<Payment> bookingPayments = booking.Payments.Where(p => !p.Deleted);
            if (bookingPayments.Any())
                return bookingPayments.Select(p => new LibBookingService.Dtos.Payment
                {
                    Id = p.Id,
                    BookingId = booking.Id,
                    CustomerId = p.Customer_id,
                    PaymentMethod = new LibBookingService.Dtos.PaymentMethod
                    {
                        Id = p.PaymentMethod.Id,
                        Name = p.PaymentMethod.Name,
                        Active = p.PaymentMethod.Active
                    },
                    Amount = p.Amount,
                    Comments = p.Comments
                });
            return Enumerable.Empty<LibBookingService.Dtos.Payment>();
        }

        /// <summary>
        /// Returns a list of booking menu items from the booking parameter.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        private IEnumerable<LibBookingService.Dtos.BookingMenuItem> GetBookingMenuItemsForBooking(Booking booking)
        {
            IEnumerable<BookingMenuItem> bookingMenuItems = booking.BookingMenuItems.Where(p => !p.Deleted);
            if (bookingMenuItems.Any())
                return bookingMenuItems.Select(mi => new LibBookingService.Dtos.BookingMenuItem
                {
                    Id = mi.Id,
                    BookingId = booking.Id,
                    MenuItemId = mi.MenuItem_id,
                    Quantity = mi.Quantity
                });
            return Enumerable.Empty<LibBookingService.Dtos.BookingMenuItem>();
        }
    }
}
