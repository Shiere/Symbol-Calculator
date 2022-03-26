﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbol_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* 처음에는 모두 잠금 */
            grp_Vanish.Enabled = false;
            chk_Reverse.Enabled = false;
            grp_Chewchew.Enabled = false;
            chk_Yumyum.Enabled = false;
            grp_Recheln.Enabled = false;
            grp_Arcana.Enabled = false;
            grp_Morass.Enabled = false;
            grp_Espera.Enabled = false;
            grp_Cernium.Enabled = false;
            chk_BurningCernium.Enabled = false;
            grp_Arcs.Enabled = false;

            /* 레벨 콤보박스 설정 */
            cmb_Vanish_LV.SelectedIndex = 0;
            cmb_Chewchew_LV.SelectedIndex = 0;
            cmb_Recheln_LV.SelectedIndex = 0;
            cmb_Arcana_LV.SelectedIndex = 0;
            cmb_Morass_LV.SelectedIndex = 0;
            cmb_Espera_LV.SelectedIndex = 0;
            cmb_Cernium_LV.SelectedIndex = 0;
            cmb_Arcs_LV.SelectedIndex = 0;

            /* 무토 콤보박스 설정 */
            cmb_ChewChew_Party.Enabled = false;
            cmb_ChewChew_Party.SelectedIndex = 2;
        }

        /* 심볼 사용칸 체크 */
        private void chk_Vanish_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Vanish.Enabled = !grp_Vanish.Enabled;
            chk_Reverse.Enabled = !chk_Reverse.Enabled;
        }

        private void chk_Chewchew_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Chewchew.Enabled = !grp_Chewchew.Enabled;
            chk_Yumyum.Enabled = !chk_Yumyum.Enabled;
        }

        private void chk_Recheln_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Recheln.Enabled = !grp_Recheln.Enabled;
        }

        private void chk_Arcana_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Arcana.Enabled = !grp_Arcana.Enabled;
        }

        private void chk_Morass_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Morass.Enabled = !grp_Morass.Enabled;
        }

        private void chk_Espera_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Espera.Enabled = !grp_Espera.Enabled;
        }

        private void chk_Cernium_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Cernium.Enabled = !grp_Cernium.Enabled;
            chk_BurningCernium.Enabled = !chk_BurningCernium.Enabled;

            /* 불르니움 스토리 깼는지도 확인. 깼으면 퀘스트 체크박스 사용가능 */
            if (chk_BurningCernium.Checked)
                chk_BurningCernium_Quest.Enabled = true;
            else
                chk_BurningCernium_Quest.Enabled = false;
        }

        private void chk_BurningCernium_CheckedChanged(object sender, EventArgs e)
        {
            /* 퀘스트 체크박스 이용여부 변환 */
            chk_BurningCernium_Quest.Enabled = !chk_BurningCernium_Quest.Enabled;
        }

        private void chk_Arcs_CheckedChanged(object sender, EventArgs e)
        {
            /* 그룹박스 이용여부 변환 */
            grp_Arcs.Enabled = !grp_Arcs.Enabled;
        }

        /* 무토 체크 */
        private void chk_Chewchew_Party_CheckedChanged(object sender, EventArgs e)
        {
            /* 콤보박스 이용여부 변환 */
            cmb_ChewChew_Party.Enabled = !cmb_ChewChew_Party.Enabled;
            calFinChewchew(sender, e);
        }

        /* 누적 성장치 값 */
        int[] expGrowth = { 0, 12, 27, 47, 74, 110, 157, 217, 292, 384, 495, 627, 782, 962, 1169, 1405, 1672, 1972, 2307, 2679 };
        int[] expGrowthAscentic = { 0, 29, 105, 246, 470, 795, 1239, 1820, 2556, 3465, 4565 };

        /* 심볼 레벨과 성장치 입력시 */
        private void changeVanishEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Vanish_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Vanish_EXP.Text, out i))
            {
                txt_Vanish_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Vanish_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Vanish_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Vanish_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Vanish_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Vanish_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Vanish_EXP.Text != "")
                EXP = Int32.Parse(txt_Vanish_EXP.Text);
            else
                EXP = 0;

            lbl_Vanish_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(EXP * 100 / maxEXP, 100) + "%)";

            calFinVanish(sender, e);
        }

        private void changeChewchewEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Chewchew_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Chewchew_EXP.Text, out i))
            {
                txt_Chewchew_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Chewchew_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Chewchew_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Chewchew_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Chewchew_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Chewchew_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Chewchew_EXP.Text != "")
                EXP = Int32.Parse(txt_Chewchew_EXP.Text);
            else
                EXP = 0;

            lbl_Chewchew_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinChewchew(sender, e);
        }

        private void changeRechelnEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Recheln_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Recheln_EXP.Text, out i))
            {
                txt_Recheln_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Recheln_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Recheln_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Recheln_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Recheln_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Recheln_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Recheln_EXP.Text != "")
                EXP = Int32.Parse(txt_Recheln_EXP.Text);
            else
                EXP = 0;

            lbl_Recheln_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinRecheln(sender, e);
        }

        private void changeArcanaEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Arcana_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Arcana_EXP.Text, out i))
            {
                txt_Arcana_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Arcana_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Arcana_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Arcana_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Arcana_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Arcana_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Arcana_EXP.Text != "")
                EXP = Int32.Parse(txt_Arcana_EXP.Text);
            else
                EXP = 0;

            lbl_Arcana_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinArcana(sender, e);
        }

        private void changeMorassEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Morass_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Morass_EXP.Text, out i))
            {
                txt_Morass_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Morass_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Morass_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Morass_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Morass_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Morass_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Morass_EXP.Text != "")
                EXP = Int32.Parse(txt_Morass_EXP.Text);
            else
                EXP = 0;

            lbl_Morass_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinMorass(sender, e);
        }

        private void changeEsperaEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Espera_LV.SelectedIndex + 1;
            int maxEXP = level * level + 11;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Espera_EXP.Text, out i))
            {
                txt_Espera_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Espera_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 19레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 19)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Espera_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[19] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Espera_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Espera_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Espera_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Espera_EXP.Text != "")
                EXP = Int32.Parse(txt_Espera_EXP.Text);
            else
                EXP = 0;

            lbl_Espera_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinEspera(sender, e);
        }

        private void changeCerniumEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Cernium_LV.SelectedIndex + 1;
            int maxEXP = 9 * level * level + 20 * level;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Cernium_EXP.Text, out i))
            {
                txt_Cernium_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Cernium_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 10레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 10)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Cernium_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[10] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Cernium_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Cernium_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Cernium_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Cernium_EXP.Text != "")
                EXP = Int32.Parse(txt_Cernium_EXP.Text);
            else
                EXP = 0;

            lbl_Cernium_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(EXP * 100 / maxEXP, 100) + "%)";

            calFinCernium(sender, e);
        }

        private void changeArcsEXPCal(object sender, EventArgs e)
        {
            int level = cmb_Arcs_LV.SelectedIndex + 1;
            int maxEXP = 9 * level * level + 20 * level;    // 요구 성장치
            int EXP;    // 현재 성장치

            /* 예외처리: 성장치 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Arcs_EXP.Text, out i))
            {
                txt_Arcs_EXP.Text = i.ToString();
                EXP = Int32.Parse(txt_Arcs_EXP.Text);

                /* 성장치 입력 범위 초과 */
                if (EXP > maxEXP)
                {
                    /* 10레벨 이라면 이미 졸업한 것 이므로 에러 메시지 출력 */
                    if (level == 10)
                    {
                        MessageBox.Show("심볼이 이미 최대치로 성장했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Arcs_EXP.Clear();
                    }
                    else if (EXP > expGrowth[19] - expGrowth[level - 1])
                    {
                        MessageBox.Show("성장치는 0 이상 " + (expGrowth[10] - expGrowth[level - 1]) + " 이하가 되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_Arcs_EXP.Clear();
                    }
                }
            }
            else
            {
                if (txt_Arcs_EXP.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Arcs_EXP.Clear();
                }
            }

            /* 예외처리: 현재 성장치 입력이 안 되어 있으면 */
            if (txt_Arcs_EXP.Text != "")
                EXP = Int32.Parse(txt_Arcs_EXP.Text);
            else
                EXP = 0;

            lbl_Arcs_EXPCal.Text = EXP + " / " + maxEXP + " (" + Math.Min(100, EXP * 100 / maxEXP) + "%)";

            calFinArcs(sender, e);
        }

        /* 드브 층수 입력 */
        private void txt_Recheln_Party_TextChanged(object sender, EventArgs e)
        {
            /* 예외처리: 층수 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Recheln_Party.Text, out i))
            {
                txt_Recheln_Party.Text = i.ToString();
            }
            else
            {
                if (txt_Recheln_Party.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Recheln_Party.Text = "";
                }
            }

            /* 아무것도 안 적혀 있다면 0이 입력되게 한다 */
            if (txt_Recheln_Party.Text == "")
            {
                txt_Recheln_Party.Text = "0";
                txt_Recheln_Party.Select(txt_Recheln_Party.Text.Length, 0); // 커서 끝으로 이동
            }

            calFinRecheln(sender, e);
        }

        /* 스세 점수 입력 */
        private void txt_Arcana_Party_TextChanged(object sender, EventArgs e)
        {
            /* 예외처리: 점수 입력을 이상하게 한다면 */
            int i;
            if (int.TryParse(txt_Arcana_Party.Text, out i))
            {
                txt_Arcana_Party.Text = i.ToString();
            }
            else
            {
                if (txt_Arcana_Party.Text != "")
                {
                    MessageBox.Show("정수를 입력해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_Arcana_Party.Text = "";
                }
            }

            /* 아무것도 안 적혀 있다면 0이 입력되게 한다 */
            if (txt_Arcana_Party.Text == "")
            {
                txt_Arcana_Party.Text = "0";
                txt_Arcana_Party.Select(txt_Arcana_Party.Text.Length, 0); // 커서 끝으로 이동
            }

            calFinArcana(sender, e);
        }

        /* 심볼 졸업 계산 */
        private double calFinDay(int dailySymbolAmount, int lv, int exp)
        {
            if (dailySymbolAmount <= 0)
                return 0;
            else
                return Math.Ceiling((double)((2679 - expGrowth[lv] - exp) / (double)dailySymbolAmount));
        }

        private double calFinDay(double dailySymbolAmount, int lv, int exp)
        {
            if (dailySymbolAmount == 0)
                return 0;
            else
                return Math.Ceiling((double)(2679 - expGrowth[lv] - exp) / dailySymbolAmount);
        }

        private double calFinDayAscentic(int dailySymbolAmount, int lv, int exp)
        {
            if (dailySymbolAmount <= 0)
                return 0;
            else
                return Math.Ceiling((double)((4565 - expGrowthAscentic[lv] - exp) / (double)dailySymbolAmount));
        }

        private int calMesoVanish(int lv)
        {
            int res = 0;

            for (int i = lv; i < 20; i++)
                res += 3110000 + 3960000 * i;

            return res;
        }

        private int calMesoChewchew(int lv)
        {
            int res = 0;

            for (int i = lv; i < 20; i++)
                res += 6220000 + 4620000 * i;

            return res;
        }

        private int calMesoRacheln(int lv)
        {
            int res = 0;

            for (int i = lv; i < 20; i++)
                res += 9330000 + 5280000 * i;

            return res;
        }

        private int calMeso(int lv)
        {
            int res = 0;

            for (int i = lv; i < 20; i++)
                res += 11196000 + 5940000 * i;

            return res;
        }

        private int calMesoAscentic(int lv)
        {
            int res = 0;

            for (int i = lv; i < 20; i++)
                res += 99000000 + 88500000 * i;

            return res;
        }

        /* 각 심볼 별 졸업 계산 */
        private void calFinVanish(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Vanish_EXP.Text != "")
                EXP = Int32.Parse(txt_Vanish_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Vanish_Quest.Checked)
            {
                /* 리버스 스토리 깨면 2배 */
                if (chk_Reverse.Checked)
                    dailySymbolAmount += 16;
                else
                    dailySymbolAmount += 8;
            }

            if (chk_Vanish_Party.Checked)
                dailySymbolAmount += 6;

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDay(dailySymbolAmount, cmb_Vanish_LV.SelectedIndex, EXP);
                lbl_Vanish_FinDay.Text = day + " 일";
                lbl_Vanish_Meso.Text = string.Format("{0:n0}", calMesoVanish(cmb_Vanish_LV.SelectedIndex + 1)) + " 메소";
                lbl_Vanish_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Vanish_FinDay.Text = "---- 일";
                lbl_Vanish_Meso.Text = "----------- 메소";
                lbl_Vanish_FinDate.Text = "----.--.--";
            }
        }

        private void calFinChewchew(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Chewchew_EXP.Text != "")
                EXP = Int32.Parse(txt_Chewchew_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Chewchew_Quest.Checked)
            {
                /* 얌얌 스토리 깨면 2배 */
                if (chk_Yumyum.Checked)
                    dailySymbolAmount += 8;
                else
                    dailySymbolAmount += 4;
            }

            if (chk_Chewchew_Party.Checked)
            {
                /* 무토 난이도에 따라 심볼 지급량이 다름*/
                switch (cmb_ChewChew_Party.SelectedIndex)
                {
                    case 0: dailySymbolAmount += 3; break;
                    case 1: dailySymbolAmount += 9; break;
                    case 2: dailySymbolAmount += 15; break;
                }
            }

            /* 계산 */
            if(dailySymbolAmount != 0 )
            {
                double day = calFinDay(dailySymbolAmount, cmb_Chewchew_LV.SelectedIndex, EXP);
                lbl_Chewchew_FinDay.Text = day + " 일";
                lbl_Chewchew_Meso.Text = string.Format("{0:n0}", calMesoChewchew(cmb_Chewchew_LV.SelectedIndex + 1)) + " 메소";
                lbl_Chewchew_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Chewchew_FinDay.Text = "---- 일";
                lbl_Chewchew_Meso.Text = "----------- 메소";
                lbl_Chewchew_FinDate.Text = "----.--.--";
            }
        }

        private void calFinRecheln(object sender, EventArgs e)
        {
            double dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Recheln_EXP.Text != "")
                EXP = Int32.Parse(txt_Recheln_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Recheln_Quest.Checked)
                dailySymbolAmount += 8;

            /* 드브 층수에 따라 심볼량이 다름 */
            /* 30층당 1개. 총 3판 돌고, 드브 랭커한테서 평균 코인 30개 받을 수 있음. */
            double dreamAmount = Int32.Parse(txt_Recheln_Party.Text) / 30.0;
            dreamAmount *= 3.0;
            dreamAmount += 1.0;

            /* 층수가 0이면 코인 없음 */
            if (txt_Recheln_Party.Text == "0" || txt_Recheln_Party.Text == "")
                dreamAmount = 0;

            dailySymbolAmount += Math.Min(500 / 3.0 + 1, dreamAmount);

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDay(dailySymbolAmount, cmb_Recheln_LV.SelectedIndex, EXP);
                lbl_Recheln_FinDay.Text = day + " 일";
                lbl_Recheln_Meso.Text = string.Format("{0:n0}", calMesoRacheln(cmb_Recheln_LV.SelectedIndex + 1)) + " 메소";
                lbl_Recheln_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Recheln_FinDay.Text = "---- 일";
                lbl_Recheln_Meso.Text = "----------- 메소";
                lbl_Recheln_FinDate.Text = "----.--.--";
            }
        }

        private void calFinArcana(object sender, EventArgs e)
        {
            double dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Arcana_EXP.Text != "")
                EXP = Int32.Parse(txt_Arcana_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Arcana_Quest.Checked)
                dailySymbolAmount += 8;

            /* 스세 점수에 따라 심볼량이 다름 */
            /* 1000점당 1개. 총 3판 돌 수 있음. */
            double saviorAmount = Int32.Parse(txt_Arcana_Party.Text) / 1000;
            saviorAmount *= 3;

            dailySymbolAmount += Math.Min(10, saviorAmount);

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDay(dailySymbolAmount, cmb_Arcana_LV.SelectedIndex, EXP);
                lbl_Arcana_FinDay.Text = day + " 일";
                lbl_Arcana_Meso.Text = string.Format("{0:n0}", calMeso(cmb_Arcana_LV.SelectedIndex + 1)) + " 메소";
                lbl_Arcana_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Arcana_FinDay.Text = "---- 일";
                lbl_Arcana_Meso.Text = "----------- 메소";
                lbl_Arcana_FinDate.Text = "----.--.--";
            }
        }

        private void calFinMorass(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Morass_EXP.Text != "")
                EXP = Int32.Parse(txt_Morass_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Morass_Quest.Checked)
                dailySymbolAmount += 8;

            if (chk_Morass_Party.Checked)
                dailySymbolAmount += 6;

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDay(dailySymbolAmount, cmb_Morass_LV.SelectedIndex, EXP);
                lbl_Morass_FinDay.Text = day + " 일";
                lbl_Morass_Meso.Text = string.Format("{0:n0}", calMeso(cmb_Morass_LV.SelectedIndex + 1)) + " 메소";
                lbl_Morass_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Morass_FinDay.Text = "---- 일";
                lbl_Morass_Meso.Text = "----------- 메소";
                lbl_Morass_FinDate.Text = "----.--.--";
            }
        }

        private void calFinEspera(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Espera_EXP.Text != "")
                EXP = Int32.Parse(txt_Espera_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Espera_Quest.Checked)
                dailySymbolAmount += 8;

            if (chk_Espera_Party.Checked)
                dailySymbolAmount += 6;

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDay(dailySymbolAmount, cmb_Espera_LV.SelectedIndex, EXP);
                lbl_Espera_FinDay.Text = day + " 일";
                lbl_Espera_Meso.Text = string.Format("{0:n0}", calMeso(cmb_Espera_LV.SelectedIndex + 1)) + " 메소";
                lbl_Espera_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Espera_FinDay.Text = "---- 일";
                lbl_Espera_Meso.Text = "----------- 메소";
                lbl_Espera_FinDate.Text = "----.--.--";
            }
        }

        private void calFinCernium(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Cernium_EXP.Text != "")
                EXP = Int32.Parse(txt_Cernium_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Cernium_Quest.Checked)
                dailySymbolAmount += 5;
            if (chk_BurningCernium.Checked)
                dailySymbolAmount += 5;

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDayAscentic(dailySymbolAmount, cmb_Cernium_LV.SelectedIndex, EXP);
                lbl_Cernium_FinDay.Text = day + " 일";
                lbl_Cernium_Meso.Text = string.Format("{0:n0}", calMesoAscentic(cmb_Cernium_LV.SelectedIndex + 1)) + " 메소";
                lbl_Cernium_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Cernium_FinDay.Text = "---- 일";
                lbl_Cernium_Meso.Text = "----------- 메소";
                lbl_Cernium_FinDate.Text = "----.--.--";
            }
        }

        private void calFinArcs(object sender, EventArgs e)
        {
            int dailySymbolAmount = 0;  // 하루에 받을 수 있는 심볼량
            int EXP;

            if (txt_Arcs_EXP.Text != "")
                EXP = Int32.Parse(txt_Arcs_EXP.Text);
            else
                EXP = 0;

            /* 일일 획득 심볼량 계산 */
            if (chk_Arcs_Quest.Checked)
                dailySymbolAmount += 5;

            /* 계산 */
            if (dailySymbolAmount != 0)
            {
                double day = calFinDayAscentic(dailySymbolAmount, cmb_Arcs_LV.SelectedIndex, EXP);
                lbl_Arcs_FinDay.Text = day + " 일";
                lbl_Arcs_Meso.Text = string.Format("{0:n0}", calMesoAscentic(cmb_Arcs_LV.SelectedIndex + 1)) + " 메소";
                lbl_Arcs_FinDate.Text = DateTime.Now.AddDays(day).ToShortDateString();
            }
            else
            {
                lbl_Arcs_FinDay.Text = "---- 일";
                lbl_Arcs_Meso.Text = "----------- 메소";
                lbl_Arcs_FinDate.Text = "----.--.--";
            }
        }
    }
}
