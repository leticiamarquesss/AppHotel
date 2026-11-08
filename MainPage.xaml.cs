
using System;
using System.Collections.Generic;

namespace AppHotel
{
    public partial class MainPage : ContentPage
    {
        private List<Reserva> reservas = new List<Reserva>();

        public MainPage()
        {
            InitializeComponent();

            CheckInPicker.MinimumDate = DateTime.Today;
            CheckOutPicker.MinimumDate = DateTime.Today.AddDays(1);

            CheckInPicker.Date = DateTime.Today;
            CheckOutPicker.Date = DateTime.Today.AddDays(1);

            BookingList.ItemsSource = reservas;
        }

        private void OnHomeClicked(object? sender, EventArgs e)
        {
            HomeView.IsVisible = true;
            AboutView.IsVisible = false;
        }

        private void OnAboutClicked(object? sender, EventArgs e)
        {
            HomeView.IsVisible = false;
            AboutView.IsVisible = true;
        }

        private void OnGuestsChanged(object? sender, ValueChangedEventArgs e)
        {
            int quantidade = (int)e.NewValue;

            GuestsLabel.Text = quantidade == 1
                ? "1 hóspede"
                : $"{quantidade} hóspedes";
        }

        private async void OnReservarClicked(object? sender, EventArgs e)
        {
            string nome = NameEntry.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nome))
            {
                await DisplayAlert(
                    "Atenção",
                    "Digite o nome do hóspede.",
                    "OK");

                return;
            }

            if (RoomPicker.SelectedItem == null)
            {
                await DisplayAlert(
                    "Atenção",
                    "Selecione uma acomodação.",
                    "OK");

                return;
            }

            DateTime checkIn =
                CheckInPicker.Date ?? DateTime.Today;

            DateTime checkOut =
                CheckOutPicker.Date ?? DateTime.Today.AddDays(1);

            if (checkIn < DateTime.Today)
            {
                await DisplayAlert(
                    "Data inválida",
                    "O check-in não pode ser anterior à data de hoje.",
                    "OK");

                return;
            }

            if (checkOut <= checkIn)
            {
                await DisplayAlert(
                    "Data inválida",
                    "O check-out deve acontecer depois do check-in.",
                    "OK");

                return;
            }

            Reserva novaReserva = new Reserva(
                nome,
                RoomPicker.SelectedItem.ToString()!,
                checkIn,
                checkOut,
                (int)GuestsStepper.Value
            );

            reservas.Add(novaReserva);

            BookingList.ItemsSource = null;
            BookingList.ItemsSource = reservas;

            await DisplayAlert(
                "Reserva realizada!",
                $"A reserva de {nome} foi cadastrada com sucesso.",
                "OK");

            NameEntry.Text = "";
            RoomPicker.SelectedItem = null;
            GuestsStepper.Value = 2;
            GuestsLabel.Text = "2 hóspedes";
        }
    }
}
