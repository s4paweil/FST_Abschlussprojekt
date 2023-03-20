namespace Boa
{
    public class Committer
    {
        private string _name;
        private int _numberCommitts;

        public Committer(string name, int numberCommitts)
        {
            this._name = name;
            this._numberCommitts = numberCommitts;
        }

        public string Name { get => _name; set => _name = value; }
        public int NumberCommitts { get => _numberCommitts; set => _numberCommitts = value; }

        public override string ToString()
        {
            return "Name: " + this.Name + ", Committs: " + this.NumberCommitts;
        }
    }
}