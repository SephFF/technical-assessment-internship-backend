﻿namespace TechnicalAssesmentBackendDeveloper;

internal sealed class Booking
{
    public string GuestName { get; private set; } = string.Empty;
    public string RoomNumber { get; private set; } = string.Empty;

    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }

    public int TotalDays { get; private set; }
    public double RatePerDay { get; private set; }
    public double DiscountPercent { get; private set; }
    public double TotalAmount { get; private set; }

    public async Task BookRoomAsync(
        string guestName,
        string roomNumber,
        DateTime checkIn,
        DateTime checkOut,
        double ratePerDay,
        double discountPercent)
    {
        ValidateInputs(guestName, roomNumber, checkIn, checkOut, ratePerDay, discountPercent);

        GuestName = guestName;
        RoomNumber = roomNumber;
        CheckInDate = checkIn;
        CheckOutDate = checkOut;
        RatePerDay = ratePerDay;
        DiscountPercent = discountPercent;

        TotalDays = (CheckOutDate.Date - CheckInDate.Date).Days;

        var gross = TotalDays * RatePerDay;
        TotalAmount = gross - (gross * DiscountPercent / 100.0);

        await LogBookingDetailsAsync();
        PrintSummary();
    }

    public void Cancel()
    {
        GuestName = string.Empty;
        RoomNumber = string.Empty;
        CheckInDate = default;
        CheckOutDate = default;
        TotalDays = 0;
        RatePerDay = 0;
        DiscountPercent = 0;
        TotalAmount = 0;

        Console.WriteLine("Booking cancelled");
    }

    private static void ValidateInputs(
        string guestName,
        string roomNumber,
        DateTime checkIn,
        DateTime checkOut,
        double ratePerDay,
        double discountPercent)
    {
        if (string.IsNullOrWhiteSpace(guestName))
            throw new ArgumentException("Guest name cannot be empty.", nameof(guestName));

        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new ArgumentException("Room number cannot be empty.", nameof(roomNumber));

        if (checkOut <= checkIn)
            throw new ArgumentException("Check-out date must be after check-in date.");

        if (ratePerDay <= 0)
            throw new ArgumentOutOfRangeException(nameof(ratePerDay), "Rate per day must be greater than 0.");

        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount must be between 0 and 100.");
    }

    private static async Task LogBookingDetailsAsync()
    {
        // Simulate writing to a log file or remote system
        await Task.Delay(1000);
        Console.WriteLine("Booking log saved.");
    }

    private void PrintSummary()
    {
        Console.WriteLine($"Room Booked for {GuestName}");
        Console.WriteLine($"Room No: {RoomNumber}");
        Console.WriteLine($"Check-In: {CheckInDate}");
        Console.WriteLine($"Check-Out: {CheckOutDate}");
        Console.WriteLine($"Total Days: {TotalDays}");
        Console.WriteLine($"Amount: {TotalAmount}");
    }
}

internal static class AppHost
{
    // Demo runner for this file (not called by Program.Main)
    public static async Task RunAsync()
    {
        var booking = new Booking();

        await booking.BookRoomAsync(
            "Alice",
            "101",
            DateTime.Now,
            DateTime.Now.AddDays(3),
            150.5,
            10);

        booking.Cancel();
    }
}
