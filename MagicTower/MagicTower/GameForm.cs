using System;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public partial class GameForm : Form
    {
        private GameModel gameModel;
        private PlayerView playerView;

        public GameForm()
        {
            gameModel = new GameModel();
            playerView = new PlayerView(gameModel.Player);
        }

        protected override void OnLoad(EventArgs e)
        {
            InitializeComponent();
            Text = "Magic Tower";
            WindowState = FormWindowState.Normal;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            playerView.Draw(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                gameModel.Player.Move(Directions.Left);
            if (e.KeyCode == Keys.D)
                gameModel.Player.Move(Directions.Right);
            if (e.KeyCode == Keys.W)
                gameModel.Player.Move(Directions.Up);
            if (e.KeyCode == Keys.S)
                gameModel.Player.Move(Directions.Down);
            Invalidate();
        }
    }

    public class PlayerView
    {
        private Player player;
        private Image playerSprite;

        public PlayerView(Player player)
        {
            this.player = player;
            playerSprite =
                Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\player.png");
        }

        public void Draw(Graphics e)
        {
            e.DrawImage(playerSprite, new Point(player.PosX, player.PosY));
        }
    }
}