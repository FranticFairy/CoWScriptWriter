using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoWGenerator
{
    class Writer
    {
        StreamWriter writer;

        string id;
        string name;
        string desc;
        string cost;
        string unitType;
        string moveType;
        string W1_ID;
        string W1_AMMO;
        string W1_TYPE;
        string W2_ID;
        string W2_AMMO;
        string W2_TYPE;
        string Fuel;
        string Move;
        string RangeMin;
        string RangeMax;
        string Vision;
        string MoveAndFire;
        int Capacity;
        string BAQ; //BattleAnimation Quantity
        string ActionList;
        string TransportList;
        int FuelCost;
        string Explosion;
        string NoTerrain;
        string BG;
        string moveSound;
        string explosionSound;
        string BGB;
        string VH; //Vision Height

        public void Initiate(string inputLine, string saveLocation)
        {
            //GET ALL DATA
            string[] input;
            input = inputLine.Split(',');

            id = input[0];
            name = input[1];
            desc = input[2].Replace('|',',');
            cost = input[3];
            unitType = input[4];
            moveType = input[5];
            W1_ID = input[6];
            W1_AMMO = input[7];
            W1_TYPE = input[8];
            W2_ID = input[9];
            W2_AMMO = input[10];
            W2_TYPE = input[11];
            Fuel = input[12];
            Move = input[13];
            RangeMin = input[14];
            RangeMax = input[15];
            Vision = input[16];
            MoveAndFire = input[17].ToLower();
            if(input[18] != "")
            {
                Capacity = int.Parse(input[18]);
            } else
            {
                Capacity = 0;
            }
            BAQ = input[19];
            ActionList = input[20].Replace('|', ',');

            StringBuilder builder = new StringBuilder(ActionList);
            builder.Replace("\"\"\"", "\"");
            builder.Replace("\"\"", "\"");

            ActionList = builder.ToString();

            TransportList = input[21].Replace('|', ',');

            builder = new StringBuilder(TransportList);
            builder.Replace("\"\"\"", "\"");
            builder.Replace("\"\"", "\"");

            TransportList = builder.ToString();
            if (input[22] != "")
            {
                FuelCost = int.Parse(input[22]);
            }
            else
            {
                FuelCost = 0;
            }
            Explosion = input[23];
            NoTerrain = input[24];
            BG = input[25];
            moveSound = input[26];
            explosionSound = input[27];
            BGB = input[28];
            VH = input[29];

            writer = new StreamWriter(saveLocation + "\\units\\" + id + ".js");
            writer.WriteLine("var Constructor = function()");
            writer.WriteLine("{");
            writer.WriteLine("");

            writeInit();
            writeLoadSprites();
            writeType();
            writeInfo();
            writeWeaponDetails();

            if(Capacity > 0)
            {
                writeCapacity();
            }

            if(ActionList != "")
            {
                writer.WriteLine("    this.actionList = [" + ActionList + "];");
            }

            writeStartOfTurn();
            writeSounds();

            if(NoTerrain == "TRUE")
            {
                writeNoTerrain();
            }

            if(BG != "")
            {
                writeBG();
            }

            writer.WriteLine("}");
            writer.WriteLine("");
            writer.WriteLine("Constructor.prototype = UNIT;");
            writer.WriteLine("var " + id + " = new Constructor();");
            writer.Close();

            StreamWriter baWriter = new StreamWriter(saveLocation + "\\battleanimations\\BATTLEANIMATION_" + id + ".js");
            baWriter.WriteLine("var Constructor = function()");
            baWriter.WriteLine("{");
            baWriter.WriteLine("    this.getMaxUnitCount = function()");
            baWriter.WriteLine("    {");
            baWriter.WriteLine("        return " + BAQ + ";");
            baWriter.WriteLine("    };");
            baWriter.WriteLine("    this.loadStandingAnimation = function(sprite, unit, defender, weapon)");
            baWriter.WriteLine("    {");
            baWriter.WriteLine("        sprite.loadSpriteV2(\"" + id + "+mask\",  GameEnums.Recoloring_Matrix);");
            baWriter.WriteLine("        sprite.loadSpriteV2(\"" + id + "\",  GameEnums.Recoloring_None);");
            baWriter.WriteLine("        BATTLEANIMATION_" + id + ".getMaxUnitCount(), Qt.point(0, 10);");
            baWriter.WriteLine("        BATTLEANIMATION.loadSpotterOrCoMini(sprite, unit, false);");
            baWriter.WriteLine("    };");
            baWriter.WriteLine("};");
            baWriter.WriteLine("");
            baWriter.WriteLine("Constructor.prototype = BATTLEANIMATION;");
            baWriter.WriteLine("var BATTLEANIMATION_" + id + " = new Constructor();");
            baWriter.Close();
        }

        public void writeInit()
        {
            writer.WriteLine("    this.init = function(unit)");
            writer.WriteLine("    {");
            if(W1_ID != "")
            {
                writer.WriteLine("        unit.setAmmo1(" + W1_AMMO + ");");
                writer.WriteLine("        unit.setMaxAmmo1(" + W1_AMMO + ");");
                writer.WriteLine("        unit.setWeapon1ID(\"" + W1_ID + "\");");
                writer.WriteLine("");
            }
            if (W2_ID != "")
            {
                writer.WriteLine("        unit.setAmmo2(" + W2_AMMO + ");");
                writer.WriteLine("        unit.setMaxAmmo2(" + W2_AMMO + ");");
                writer.WriteLine("        unit.setWeapon2ID(\"" + W2_ID + "\");");
                writer.WriteLine("");
            }

            writer.WriteLine("        unit.setFuel(" + Fuel + ");");
            writer.WriteLine("        unit.setMaxFuel(" + Fuel + ");");
            writer.WriteLine("        unit.setBaseMovementPoints(" + Move + ");");
            writer.WriteLine("        unit.setMinRange(" + RangeMin + ");");
            writer.WriteLine("        unit.setMaxRange(" + RangeMax + ");");
            writer.WriteLine("        unit.setVision(" + Vision + ");");
            if(VH != "")
            {
                writer.WriteLine("        unit.setVisionHigh(" + VH + ");");
            }
            writer.WriteLine("");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }

        public void writeInfo()
        {
            writer.WriteLine("    this.getName = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return qsTr(\"" + name +"\");");
            writer.WriteLine("    };");
            writer.WriteLine("");
            writer.WriteLine("    this.getDescription = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return qsTr(\"" + desc + "\");");
            writer.WriteLine("    };");
            writer.WriteLine("");
            writer.WriteLine("    this.getBaseCost = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return " + cost + ";");
            writer.WriteLine("    };");
            writer.WriteLine("");

        }

        public void writeLoadSprites()
        {
            writer.WriteLine("    this.loadSprites = function(unit)");
            writer.WriteLine("    {");
            writer.WriteLine("        unit.loadSpriteV2(\"" + id + "+mask\", GameEnums.Recoloring_Matrix);");
            writer.WriteLine("        unit.loadSpriteV2(\"" + id + "\", GameEnums.Recoloring_None);");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }

        public void writeType()
        {
            writer.WriteLine("    this.getMovementType = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return \"" + moveType + "\";");
            writer.WriteLine("    };");
            writer.WriteLine("");
            writer.WriteLine("    this.getUnitType = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return "+unitType+";");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }

        public void writeWeaponDetails()
        {
            writer.WriteLine("	this.canMoveAndFire = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return " + MoveAndFire + ";");
            writer.WriteLine("    };");
            writer.WriteLine("");
            if(W1_TYPE != "")
            {
                writer.WriteLine("    this.getTypeOfWeapon1 = function(unit)");
                writer.WriteLine("    {");
                writer.WriteLine("        return " + W1_TYPE + ";");
                writer.WriteLine("    };");
                writer.WriteLine("");
            }
            if (W2_TYPE != "")
            {
                writer.WriteLine("    this.getTypeOfWeapon2 = function(unit)");
                writer.WriteLine("    {");
                writer.WriteLine("        return " + W2_TYPE + ";");
                writer.WriteLine("    };");
                writer.WriteLine("");
            }

        }

        public void writeCapacity()
        {
            writer.WriteLine("    this.getLoadingPlace = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return " + Capacity + ";");
            writer.WriteLine("    };");
            writer.WriteLine("    this.transportList = [" + TransportList + "];");
            writer.WriteLine("");
        }

        public void writeStartOfTurn()
        {
            writer.WriteLine("    this.startOfTurn = function(unit, map)");
            writer.WriteLine("    {");
            writer.WriteLine("        if (unit.getTerrain() !== null)");
            writer.WriteLine("        {");

            if(FuelCost > 0)
            {
                writer.WriteLine("            //Start of Turn Fuel Cost");
                writer.WriteLine("            var fuelCosts = " + FuelCost + " + unit.getFuelCostModifier(Qt.point(unit.getX(), unit.getY()), " + FuelCost + ");");
                writer.WriteLine("            if (fuelCosts < 0)");
                writer.WriteLine("            {");
                writer.WriteLine("                fuelCosts = 0;");
                writer.WriteLine("            }");
                writer.WriteLine("            unit.setFuel(unit.getFuel() - fuelCosts);");
            }

            writer.WriteLine("        }");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }

        public void writeSounds()
        {
            writer.WriteLine("    this.doWalkingAnimation = function(action, map)");
            writer.WriteLine("    {");
            writer.WriteLine("        var unit = action.getTargetUnit();");
            writer.WriteLine("        var animation = GameAnimationFactory.createWalkingAnimation(map, unit, action);");
            writer.WriteLine("        var unitID = unit.getUnitID().toLowerCase();");
            writer.WriteLine("        animation.setSound(\"" + moveSound + ".wav\", -2);");
            writer.WriteLine("        return animation;");
            writer.WriteLine("    };");

            if(Explosion != "" && explosionSound != "")
            {
                writer.WriteLine("");
                writer.WriteLine("    this.createExplosionAnimation = function(x, y, unit, map)");
                writer.WriteLine("    {");
                writer.WriteLine("        var animation = GameAnimationFactory.createAnimation(map, x, y);");
                writer.WriteLine("        animation.addSprite(\"explosion + " + Explosion + "\", -map.getImageSize() / 2, -map.getImageSize(), 0, 2);");
                writer.WriteLine("        animation.setSound(\"explosion + " + explosionSound + ".wav\");");
                writer.WriteLine("        return animation;");
                writer.WriteLine("    };");
            }

            writer.WriteLine("");

        }

        public void writeNoTerrain()
        {
            writer.WriteLine("    this.useTerrainDefense = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return false;");
            writer.WriteLine("    };");
            writer.WriteLine("");
            writer.WriteLine("    this.useTerrainHide = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return false;");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }

        public void writeBG()
        {
            writer.WriteLine("	this.getTerrainAnimationBase = function(unit, terrain, defender, map)");
            writer.WriteLine("    {");
            writer.WriteLine("        var weatherModifier = TERRAIN.getWeatherModifier(map);");
            writer.WriteLine("        return \"base_\" + weatherModifier + \"" + BG + "\";");
            writer.WriteLine("    };");
            writer.WriteLine("");
            writer.WriteLine("    this.getTerrainAnimationForeground = function(unit, terrain, defender, map)");
            writer.WriteLine("    {");
            writer.WriteLine("        return \"\";");
            writer.WriteLine("    };");
            writer.WriteLine("");
            if(BGB != "")
            {
                writer.WriteLine("    this.getTerrainAnimationBackground = function(unit, terrain, defender, map)");
                writer.WriteLine("    {");
                writer.WriteLine("        var weatherModifier = TERRAIN.getWeatherModifier(map);");
                writer.WriteLine("        return \"back_\" + weatherModifier +\"" + BGB + "\";");
                writer.WriteLine("    };");
            } else
            {
                writer.WriteLine("    this.getTerrainAnimationBackground = function(unit, terrain, defender, map)");
                writer.WriteLine("    {");
                writer.WriteLine("    };");
            }

            writer.WriteLine("");
            writer.WriteLine("    this.getTerrainAnimationMoveSpeed = function()");
            writer.WriteLine("    {");
            writer.WriteLine("        return 1;");
            writer.WriteLine("    };");
            writer.WriteLine("");
        }
    }
}
